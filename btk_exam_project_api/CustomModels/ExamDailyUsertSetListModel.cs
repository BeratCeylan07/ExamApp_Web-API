using System;
namespace btk_exam_project_api.CustomModels
{
	public class ExamDailyUsertSetListModel
	{
		public string userUID { get; set; }

		public string examSetUID { get; set; }

		public string studentInfo { get; set; }
		public string examInfo { get; set; }
		public int examSetStatus { get; set; }
		public DateTime examSetDate { get; set; }
	}
}

