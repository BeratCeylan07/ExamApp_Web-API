using System;
using btk_exam_project_api.Models;

namespace btk_exam_project_api.CustomModels
{
	public class StudentExamListModel
	{
		public int id { get; set; }

		public string uid { get; set; }

		public DenemeSinaviOturum session { get; set; }
		public string examName { get; set; }
		public string examCat { get; set; }
		public string examPub { get; set; }
		public double examAmount { get; set; }
		public int status { get; set; }
		public DateTime isCreatedDate { get; set; }
		public DateTime isModifiedDate { get; set; }
		public int isCreatedUserid { get; set; }
		public int isModifiedUserid { get; set; }
		public double? dogru { get; set; }
		public double? yanlis { get; set; }
		public double? net { get; set; }
		public bool isActive { get; set; }
	}
}

