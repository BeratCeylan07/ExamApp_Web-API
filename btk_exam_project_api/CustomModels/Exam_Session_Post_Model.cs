using System;
namespace btk_exam_project_api.CustomModels
{
	public class Exam_Session_Post_Model
	{
		public string examuid { get; set; }
		public string? examSessionUID { get; set; }
		public string sessioninfo { get; set; }
		public int kontenjan { get; set; }
		public int userid { get; set; }
		public int subeID { get; set; } = 0;
		public DateTime examdate { get; set; }
	}
}

