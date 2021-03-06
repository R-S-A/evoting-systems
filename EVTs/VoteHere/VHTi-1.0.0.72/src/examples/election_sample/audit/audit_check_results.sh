#!/bin/bash
# 
# This material is subject to the VoteHere Source Code Evaluation
# License Agreement ("Agreement").  Possession and/or use of this
# material indicates your acceptance of this Agreement in its entirety.
# Copies of the Agreement may be found at www.votehere.net.
# 
# Copyright 2004 VoteHere, Inc.  All Rights Reserved
# 
# You may not download this Software if you are located in any country
# (or are a national of a country) subject to a general U.S. or
# U.N. embargo or are deemed to be a terrorist country (i.e., Cuba,
# Iran, Iraq, Libya, North Korea, Sudan and Syria) by the United States
# (each a "Prohibited Country") or are otherwise denied export
# privileges from the United States or Canada ("Denied Person").
# Further, you may not transfer or re-export the Software to any such
# country or Denied Person without a license or authorization from the
# U.S. government.  By downloading the Software, you represent and
# warrant that you are not a Denied Person, are not located in or a
# national of a Prohibited Country, and will not export or re-export to
# any Prohibited Country or Denied Party.
set -e 

# Usage
if test $# -lt 2 ; then
   echo "Usage: audit_check_results.sh ELECTION_ID PRECINCT_ID"
   exit 2
fi
eid=$1
pid=$2

# Check "signed blank ballot" signature
echo -n "AUDIT: checking signature on SignedBlankBallot ... "
vhti_call check_xml \
   ../transcript/config/public_key_leo.xml \
   ../transcript/precinct_$eid-$pid/config/blank_ballot_signed.xml \
   \"BlankBallot\" \
   ./_result.xml ./_bb.xml
if test "$(cat _result.xml)" != "<CheckResults>0:Success</CheckResults>" ; then
   echo -n "FAILED: "
   cat _result.xml
   exit 2
fi
echo "done."

# Check "signed_voted_ballots.xml" signatures
echo "AUDIT: checking signatures on SignedVotedBallots ... "
perl audit_check_sigs_svb.pl \
   ../transcript/config/public_key_dre.xml \
   ../transcript/precinct_$eid-$pid/ballotbox/signed_voted_ballots.xml \
   ./_return.xml
if test "$(cat _return.xml)" != "<CheckResults>0:Success</CheckResults>" ; then
   echo -n "FAILED: "
   cat _return.xml
   exit 2
fi
echo "... done checking signatures on SignedVotedBallots."

# Check shuffle proofs
nshuffles=$(ls ../transcript/precinct_$eid-$pid/tabulation/raw_ballot_box* | grep -c '')
nshuffles=`expr $nshuffles - 1`
i=0
while test $i -lt $nshuffles ; do
   echo -n "AUDIT: checking shuffle proof from raw_ballot_box_$i to raw_ballot_box_`expr $i + 1` ... "
   vhti_call check_shuffle \
      ../transcript/precinct_$eid-$pid/tabulation/raw_ballot_box_$i.xml \
      ../transcript/precinct_$eid-$pid/tabulation/raw_ballot_box_`expr $i + 1`.xml \
      ../transcript/precinct_$eid-$pid/config/blank_ballot_signed.xml \
      ../transcript/config/public_key_leo.xml \
      ../transcript/precinct_$eid-$pid/tabulation/shuffle_validity_proof_$i-`expr $i + 1`.xml \
      ./_result.xml
   if test "$(cat _result.xml)" != "<CheckResults>0:Success</CheckResults>" ; then
      echo -n "FAILED: "
      cat _result.xml
      exit 2
   fi
   echo "done."

   i=`expr $i + 1`
done

# Check decryption proofs
echo -n "AUDIT: checking decryption proofs and generating clear text ballots ... "
vhti_call check_partial_decrypts_and_combine \
   ../transcript/precinct_$eid-$pid/config/blank_ballot_signed.xml \
   ../transcript/config/public_key_leo.xml \
   ../transcript/precinct_$eid-$pid/tabulation/partially_decrypted_ballot_box.xml \
   ./precinct_$eid-$pid/clear_text_ballots.xml ./_result.xml
   if test "$(cat _result.xml)" != "<CheckResults>0:Success</CheckResults>" ; then
      echo -n "FAILED: "
      cat _result.xml
      exit 2
   fi
echo "done."

# Generate election results
echo "AUDIT: generating election results ... "
perl audit_tabulate.pl \
   ../transcript/precinct_$eid-$pid/config/blank_ballot.xml \
   ./precinct_$eid-$pid/clear_text_ballots.xml \
   ./precinct_$eid-$pid/results.xml
echo "... done generating election results."

# Check election results against those published by election officials and trustees
echo "AUDIT: checking election results ... "
perl ./audit_check_results.pl \
   ./precinct_$eid-$pid/results.xml \
   ../transcript/precinct_$eid-$pid/tabulation/results.xml  \
   ./_return.xml
   if test "$(cat _return.xml)" != "<CheckResults>0:Success</CheckResults>" ; then
      echo -n "FAILED: "
      cat _return.xml
      exit 2
   fi
echo "... done checking election results."

# Clean-up
rm -f ./_*