namespace RealEstate.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using RealEstate.Models.ComplexType;

    [Table("PostLabelDatas")]
    public class PostLabelData : IEquatable<PostLabelData>
    {
        public PostLabelData()
        {
            ActivityLog = new ActivityLog();
        }

        [Key, Column(Order = 0)]
        [ForeignKey("Post")]
        public int PostId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("PostLabel")]
        public int PostLabelId { get; set; }

        public Post Post { get; set; }

        public PostLabel PostLabel { get; set; }

        public ActivityLog ActivityLog { get; set; }

        public bool Equals(PostLabelData other)
            => other.PostLabelId == PostLabelId && 
               other.PostId      == PostId;
    }
}