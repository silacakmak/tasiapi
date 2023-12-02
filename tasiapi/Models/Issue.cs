using System.ComponentModel.DataAnnotations;

namespace tasiapi.Models
{
    public class Issue
    {
        public int Id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        public Priority Priority { get; set; }
        public IssueType IssueType { get; set; }
         public DateTime Created { get; set; }
        public DateTime? Completed { get; set; } 


      }
    public enum Priority
    {
        Low,Mediium,High
    }
    public enum IssueType
    {
        Feature,Bug,Documentation
    }
    }

