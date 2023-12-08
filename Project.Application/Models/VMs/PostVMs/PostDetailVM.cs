using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.VMs.PostVMs
{
    public class PostDetailVM
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorImagePath { get; set; }
        public string AuthorFullName => AuthorFirstName + " " + AuthorLastName;
    }
}
