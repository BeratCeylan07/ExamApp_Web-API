using System;
namespace btk_exam_project_api.CustomModels
{
	public class ExamReponseModel
	{
        public bool status { get; set; }
        public string message { get; set; }
        public string title { get; set; }
        public Exam_Post_Model exam { get; set; }
    }
}

