using System.ComponentModel.DataAnnotations;

namespace Main_Common.Model.Test
{
    public class TestModel
    {
        /// <summary>
        /// No
        /// </summary>
        [Required]
        public string? No { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string? Name { get; set; }
    }
}
