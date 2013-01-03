﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace Aegis_DVL.Database
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class Entities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new Entities object using the connection string found in the 'Entities' section of the application configuration file.
        /// </summary>
        public Entities() : base("name=Entities", "Entities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new Entities object.
        /// </summary>
        public Entities(string connectionString) : base(connectionString, "Entities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new Entities object.
        /// </summary>
        public Entities(EntityConnection connection) : base(connection, "Entities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Voter> Voters
        {
            get
            {
                if ((_Voters == null))
                {
                    _Voters = base.CreateObjectSet<Voter>("Voters");
                }
                return _Voters;
            }
        }
        private ObjectSet<Voter> _Voters;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Voters EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToVoters(Voter voter)
        {
            base.AddObject("Voters", voter);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="VoterModel", Name="Voter")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Voter : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Voter object.
        /// </summary>
        /// <param name="voterNumber">Initial value of the VoterNumber property.</param>
        /// <param name="cPR">Initial value of the CPR property.</param>
        /// <param name="ballotStatus">Initial value of the BallotStatus property.</param>
        public static Voter CreateVoter(global::System.Byte[] voterNumber, global::System.Byte[] cPR, global::System.Byte[] ballotStatus)
        {
            Voter voter = new Voter();
            voter.VoterNumber = voterNumber;
            voter.CPR = cPR;
            voter.BallotStatus = ballotStatus;
            return voter;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Byte[] VoterNumber
        {
            get
            {
                return StructuralObject.GetValidValue(_VoterNumber);
            }
            set
            {
                if (!StructuralObject.BinaryEquals(_VoterNumber, value))
                {
                    OnVoterNumberChanging(value);
                    ReportPropertyChanging("VoterNumber");
                    _VoterNumber = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("VoterNumber");
                    OnVoterNumberChanged();
                }
            }
        }
        private global::System.Byte[] _VoterNumber;
        partial void OnVoterNumberChanging(global::System.Byte[] value);
        partial void OnVoterNumberChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Byte[] CPR
        {
            get
            {
                return StructuralObject.GetValidValue(_CPR);
            }
            set
            {
                OnCPRChanging(value);
                ReportPropertyChanging("CPR");
                _CPR = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("CPR");
                OnCPRChanged();
            }
        }
        private global::System.Byte[] _CPR;
        partial void OnCPRChanging(global::System.Byte[] value);
        partial void OnCPRChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Byte[] BallotStatus
        {
            get
            {
                return StructuralObject.GetValidValue(_BallotStatus);
            }
            set
            {
                OnBallotStatusChanging(value);
                ReportPropertyChanging("BallotStatus");
                _BallotStatus = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("BallotStatus");
                OnBallotStatusChanged();
            }
        }
        private global::System.Byte[] _BallotStatus;
        partial void OnBallotStatusChanging(global::System.Byte[] value);
        partial void OnBallotStatusChanged();

        #endregion
    
    }

    #endregion
    
}
