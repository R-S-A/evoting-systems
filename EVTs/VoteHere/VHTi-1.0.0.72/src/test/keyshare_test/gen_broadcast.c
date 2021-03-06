/*  */
/* This material is subject to the VoteHere Source Code Evaluation */
/* License Agreement ("Agreement").  Possession and/or use of this */
/* material indicates your acceptance of this Agreement in its entirety. */
/* Copies of the Agreement may be found at www.votehere.net. */
/*  */
/* Copyright 2004 VoteHere, Inc.  All Rights Reserved */
/*  */
/* You may not download this Software if you are located in any country */
/* (or are a national of a country) subject to a general U.S. or */
/* U.N. embargo or are deemed to be a terrorist country (i.e., Cuba, */
/* Iran, Iraq, Libya, North Korea, Sudan and Syria) by the United States */
/* (each a "Prohibited Country") or are otherwise denied export */
/* privileges from the United States or Canada ("Denied Person"). */
/* Further, you may not transfer or re-export the Software to any such */
/* country or Denied Person without a license or authorization from the */
/* U.S. government.  By downloading the Software, you represent and */
/* warrant that you are not a Denied Person, are not located in or a */
/* national of a Prohibited Country, and will not export or re-export to */
/* any Prohibited Country or Denied Party. */
// Copyright 2003 VoteHere Inc. All Rights Reserved.

#include "keyshare_test.h"
#include <vhti/gen_broadcast.h>
#include <stdio.h>
#include <malloc.h>
#include <support_test/test-data.h>

static void
expect_generate_broadcast (CuTest *tc,
                           const int expected_status,
                           KeyGenParameters const key_gen_parameters,
                           SecretCoefficients const secret_coefficients,
                           RandomState const random_state)
{
   char *broadcast_value = 0;
   char *random_state_out = 0;

   CuAssert (tc,
             (0 == expected_status ?
              "VHTI_generate_broadcast should have accepted valid input"
              : "VHTI_generate_broadcast should have rejected invalid input"),
             expected_status == VHTI_generate_broadcast (key_gen_parameters,
             secret_coefficients, random_state, &broadcast_value, &random_state_out));

   if (0 != expected_status)
   {
      if (broadcast_value)
         fprintf (stderr, "Hmm: `%s'\n", broadcast_value);
      CuAssertPtrEquals (tc, NULL, broadcast_value);

      if (random_state_out)
         fprintf (stderr, "Hmm: `%s'\n", random_state_out);
      CuAssertPtrEquals (tc, NULL, random_state_out);
   }
   else
   {
      CuAssertPtrNotNull (tc, broadcast_value);
      CuAssertPtrNotNull (tc, random_state_out);
      CuAssert (tc,
                "VHTI_generate_broadcast should have returned a BroadcastValue and a RandomState",
                0 == (VHTI_validate (BROADCAST_VALUE, broadcast_value)
                && VHTI_validate (RANDOM_STATE, random_state_out) ) );
      VHTI_free (broadcast_value);
      VHTI_free (random_state_out);
   }
}

void
gen_broadcast_validation (CuTest *tc)
{
   expect_generate_broadcast (tc, 0, valid_key_gen_parameters, valid_secret_coefficients, valid_random_state);
   expect_generate_broadcast (tc, -1, invalid_key_gen_parameters, valid_secret_coefficients, valid_random_state);
   expect_generate_broadcast (tc, -1, valid_key_gen_parameters, invalid_secret_coefficients, valid_random_state);
   expect_generate_broadcast (tc, -1, valid_key_gen_parameters, valid_secret_coefficients, invalid_random_state);
}
