// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
// using System.Linq;
//
// // This file contains all the entity classes for the data model.
// // They are structured to be used with Entity Framework Core.
//
// #region Enums
//
// /// <summary>
// /// Represents the possible roles a Person can have.
// /// As per the design document, this handles the overlapping inheritance.
// /// </summary>
// public enum PersonType
// {
//     Oligarch,
//     Politician
// }
//
// /// <summary>
// /// The status of a proposed Bill.
// /// </summary>
// public enum BillStatus
// {
//     Proposed,
//     Passed,
//     Rejected
// }
//
// /// <summary>
// /// The status of a Deal between an Oligarch and a Politician.
// /// </summary>
// public enum DealStatus
// {
//     PreScreening,
//     PendingDecision,
//     AutoRejected,
//     Declined,
//     Accepted
// }
//
// /// <summary>
// /// The political affiliation of a Political Organisation.
// /// </summary>
// public enum PoliticalPosition
// {
//     FarLeft = 0,
//     Left = 1,
//     CenterLeft = 2,
//     Center = 3,
//     CenterRight = 4,
//     Right = 5,
//     FarRight = 6
// }
//
// /// <summary>
// /// The position a politician can hold within a Party.
// /// </summary>
// public enum PartyPosition
// {
//     Leader,
//     Spokesperson,
//     Treasurer,
//     Member
// }
//
// #endregion
//
// #region Core Entities (Person, Deal, Bill)
//
// /// <summary>
// /// Represents a person in the system.
// /// This class implements the "flattening" strategy for the dynamic, overlapping
// /// inheritance of Politician and Oligarch roles.
// /// </summary>
// public class Person
// {
//     [Key]
//     public int PersonId { get; set; }
//
//     [Required]
//     [StringLength(100)]
//     public string Name { get; set; }
//
//     [Required]
//     [Range(18, 120)]
//     public int Age { get; set; }
//
//     // --- Role-specific attributes (made nullable) ---
//
//     /// <summary>
//     /// Politician's influence score. Null if the person is not a politician.
//     /// </summary>
//     [Range(1, 10, ErrorMessage = "InfluenceScore must be between 1 and 10.")]
//     public int? InfluenceScore { get; set; }
//
//     /// <summary>
//     /// Oligarch's wealth. Null if the person is not an oligarch.
//     /// </summary>
//     public double? Wealth { get; set; }
//
//     /// <summary>
//     /// Tracks the current roles of the person (Politician, Oligarch, or both).
//     /// Modeled as a collection to support overlapping roles.
//     /// In a real EF implementation, this would likely be stored as a bitmask or a separate join table.
//     /// For simplicity here, we'll represent the concept. EF may need a Value Converter for this.
//     /// </summary>
//     public ICollection<PersonType> Types { get; set; } = new List<PersonType>();
//
//     // --- Navigation Properties ---
//
//     // Associations from Politician role
//     public virtual ICollection<Bill> BillsProposed { get; set; } = new List<Bill>();
//     public virtual ICollection<Bill> BillsSupported { get; set; } = new List<Bill>();
//     public virtual ICollection<Bill> BillsOpposed { get; set; } = new List<Bill>();
//     public virtual ICollection<PartyMembership> PartyMemberships { get; set; } = new List<PartyMembership>();
//     public virtual ICollection<MovementMembership> MovementMemberships { get; set; } = new List<MovementMembership>();
//
//     // Associations from Oligarch role
//     public virtual ICollection<Business> OwnedBusinesses { get; set; } = new List<Business>();
//     
//     // Associations for Deals (can be either Oligarch or Politician)
//     [InverseProperty("ProposingOligarch")]
//     public virtual ICollection<Deal> DealsProposed { get; set; } = new List<Deal>();
//     public virtual ICollection<Deal> DalsProposed { get; set; } = new List<Deal>();
//     
//     [InverseProperty("ReceivingPolitician")]
//     public virtual ICollection<Deal> DealsReceived { get; set; } = new List<Deal>();
// }
//
// /// <summary>
// /// Represents a deal, which is an association class between a Person (as Oligarch)
// /// and another Person (as Politician).
// /// </summary>
// public class Deal
// {
//     [Key]
//     public int DealId { get; set; }
//
//     public string? Description { get; set; }
//
//     [Required]
//     public DateTime DateProposed { get; set; }
//     
//     public DateTime? DateDecided { get; set; }
//
//     [Required]
//     [Range(1, 5, ErrorMessage = "Deal Level must be between 1 and 5.")]
//     public int DealLevel { get; set; }
//
//     [Required]
//     public DealStatus Status { get; set; }
//     
//     // From Design Diagram
//     public bool AutoRejectPreApproval { get; set; } 
//
//     // --- Foreign Keys and Navigation Properties ---
//
//     [Required]
//     public int ProposingOligarchId { get; set; }
//     [ForeignKey("ProposingOligarchId")]
//     public virtual Person ProposingOligarch { get; set; }
//     
//     [Required]
//     public int ReceivingPoliticianId { get; set; }
//     [ForeignKey("ReceivingPoliticianId")]
//     public virtual Person ReceivingPolitician { get; set; }
// }
//
//
// /// <summary>
// /// Represents a legislative bill proposed by a politician.
// /// </summary>
// public class Bill
// {
//     [Key]
//     public int BillId { get; set; }
//
//     [Required]
//     [StringLength(200)]
//     public string Name { get; set; }
//     
//     public string? Description { get; set; }
//
//     [Required]
//     public BillStatus Status { get; set; }
//
//     // --- Foreign Keys and Navigation Properties ---
//
//     /// <summary>
//     /// FK for the proposing politician. Nullable because a politician can leave their role.
//     /// </summary>
//     public int? ProposerId { get; set; }
//     [ForeignKey("ProposerId")]
//     public virtual Person? Proposer { get; set; }
//
//     /// <summary>
//     /// Politicians who support this bill.
//     /// The XOR constraint with Opposers must be handled in business logic/service layer.
//     /// </summary>
//     public virtual ICollection<Person> Supporters { get; set; } = new List<Person>();
//
//     /// <summary>
//     /// Politicians who oppose this bill.
//     /// The XOR constraint with Supporters must be handled in business logic/service layer.
//     /// </summary>
//     public virtual ICollection<Person> Opposers { get; set; } = new List<Person>();
// }
//
// #endregion
//
// #region Political Organisation Hierarchy
//
// /// <summary>
// /// Abstract base class for political organisations. Implements the TPT (Table-Per-Type) inheritance strategy.
// /// </summary>
// public abstract class PoliticalOrganisation
// {
//     [Key]
//     public int OrganisationId { get; set; }
//
//     /// <summary>
//     /// The name of the political organisation. Must be unique across all organisations.
//     /// Uniqueness would be configured in DbContext.OnModelCreating using HasIndex(p => p.Name).IsUnique();
//     /// </summary>
//     [Required]
//     [StringLength(150)]
//     public string Name { get; set; }
//
//     [Required]
//     public PoliticalPosition PoliticalAffiliation { get; set; }
//
//     /// <summary>
//     /// Abstract method to be implemented by concrete subclasses (Party, Movement).
//     /// </summary>
//     public abstract double CalculateInfluence();
// }
//
// /// <summary>
// /// A concrete political organisation representing a party.
// /// </summary>
// public class Party : PoliticalOrganisation
// {
//     [Required]
//     public int NumSeatsInParliament { get; set; }
//     
//     public bool IsParticipatingInElection { get; set; }
//
//     /// <summary>
//     /// Multi-value attribute. In EF Core, this is often handled by converting
//     /// to/from a JSON string using a Value Converter, or a separate related table.
//     /// </summary>
//     public ICollection<string> PrimaryColors { get; set; } = new List<string>();
//
//     // Navigation property for the association class
//     public virtual ICollection<PartyMembership> Memberships { get; set; } = new List<PartyMembership>();
//
//     public override double CalculateInfluence()
//     {
//         // Party-specific influence calculation logic goes here.
//         // For example: return NumSeatsInParliament * 1.5;
//         throw new NotImplementedException();
//     }
// }
//
// /// <summary>
// /// A concrete political organisation representing a movement.
// /// </summary>
// public class Movement : PoliticalOrganisation
// {
//     [Required]
//     public string MainIssue { get; set; }
//     
//     [Required]
//     public string TargetDemographic { get; set; }
//
//     // Navigation property for the association class
//     public virtual ICollection<MovementMembership> Memberships { get; set; } = new List<MovementMembership>();
//     
//     // Navigation property for supporting organisations
//     public virtual ICollection<PoliticalOrganisation> SupportedBy { get; set; } = new List<PoliticalOrganisation>();
//
//     public override double CalculateInfluence()
//     {
//         // Movement-specific influence calculation logic goes here.
//         // It can polymorphically call CalculateInfluence() on supporting organisations.
//         // For example: double baseInfluence = Memberships.Count * 0.5;
//         // double supportInfluence = SupportedBy.Sum(org => org.CalculateInfluence());
//         // return baseInfluence + supportInfluence;
//         throw new NotImplementedException();
//     }
// }
//
// #endregion
//
// #region Association Classes for Memberships
//
// /// <summary>
// /// Association class for the many-to-many relationship between Person (Politician) and Party.
// /// The {BAG} constraint is supported by allowing multiple membership records for the same person-party pair.
// /// </summary>
// public class PartyMembership
// {
//     [Key]
//     public int MembershipId { get; set; }
//     
//     [Required]
//     public DateTime StartDate { get; set; }
//     
//     public DateTime? EndDate { get; set; } // Optional
//     
//     [Required]
//     public PartyPosition Position { get; set; }
//
//     // --- Foreign Keys and Navigation Properties ---
//     [Required]
//     public int PoliticianId { get; set; }
//     [ForeignKey("PoliticianId")]
//     public virtual Person Politician { get; set; }
//
//     [Required]
//     public int PartyId { get; set; }
//     [ForeignKey("PartyId")]
//     public virtual Party Party { get; set; }
// }
//
// /// <summary>
// /// Association class for the many-to-many relationship between Person (Politician) and Movement.
// /// The {BAG} constraint is supported by allowing multiple membership records for the same person-movement pair.
// /// </summary>
// public class MovementMembership
// {
//     [Key]
//     public int MembershipId { get; set; }
//
//     [Required]
//     public DateTime StartDate { get; set; }
//     
//     public DateTime? EndDate { get; set; } // Optional
//     
//     public bool IsLeader { get; set; }
//
//     // --- Foreign Keys and Navigation Properties ---
//     [Required]
//     public int PoliticianId { get; set; }
//     [ForeignKey("PoliticianId")]
//     public virtual Person Politician { get; set; }
//
//     [Required]
//     public int MovementId { get; set; }
//     [ForeignKey("MovementId")]
//     public virtual Movement Movement { get; set; }
// }
//
// #endregion
//
//
// /// <summary>
// /// Represents a business owned by a Person (as Oligarch).
// /// </summary>
// public class Business
// {
//     [Key]
//     public int BusinessId { get; set; }
//
//     [Required(ErrorMessage = "Business name is required.")]
//     [StringLength(150, MinimumLength = 1, ErrorMessage = "Business name must be between 1 and 150 characters.")]
//     public string Name { get; set; }
//
//     /// <summary>
//     /// Derived attribute, calculated on demand.
//     /// Not mapped to the database.
//     /// </summary>
//     [NotMapped]
//     public double AverageWage => Workers.Any() 
//         ? Workers.Average(worker => worker.Wage) 
//         : 0;
//
//     // --- Foreign Keys and Navigation Properties ---
//     
//     [Required]
//     public int OwnerId { get; set; }
//     [ForeignKey("OwnerId")]
//     public virtual Person Owner { get; set; }
//
//     /// <summary>
//     /// Composition: Workers are part of a Business.
//     /// OnDelete(DeleteBehavior.Cascade) would be set in DbContext to enforce this.
//     /// </summary>
//     public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
// }
//
// /// <summary>
// /// Represents a worker employed by a Business.
// /// </summary>
// public class Worker
// {
//     [Key]
//     public int WorkerId { get; set; }
//
//     /// <summary>
//     /// Worker's name is optional.
//     /// </summary>
//     public string? Name { get; set; }
//     
//     [Required]
//     public string Position { get; set; }
//     
//     /// <summary>
//     /// The worker's wage. A dynamic constraint {wage >= minimumwage}
//     /// must be enforced through validation logic in a service or via IValidatableObject.
//     /// </summary>
//     [Required]
//     public double Wage { get; set; }
//
//     /// <summary>
//     /// Static attribute, same for all workers.
//     /// </summary>
//     public static double MinimumWage { get; set; } = 15.00; 
//
//     // --- Foreign Keys and Navigation Properties ---
//
//     /// <summary>
//     /// Foreign key for the composition relationship to Business.
//     /// </summary>
//     [Required]
//     public int BusinessId { get; set; }
//     [ForeignKey("BusinessId")]
//     public virtual Business Business { get; set; }
// }
//
