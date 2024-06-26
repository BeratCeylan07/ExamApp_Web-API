using System;
using btk_exam_project_api.Models;

namespace btk_exam_project_api.CustomModels
{
	public class Exam_Session_Student_List_Model
	{
        public int SessionSetId { get; set; }
        public string SessionSetUID { get; set; }
        public DateTime IsCreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }
        public string Students { get; set; }
        public DateTime oturumTarihi { get; set; } 
        public double? dogru { get; set; }
        public double? yanlis { get; set; }
        public double? net { get; set; }
    }
}

