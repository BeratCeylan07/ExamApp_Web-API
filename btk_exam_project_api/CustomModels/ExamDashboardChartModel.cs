using System;
namespace btk_exam_project_api.CustomModels
{
	public class ExamDashboardChartModel
	{
		public string label { get; set; }
		public int y { get; set; }
		public double? yNet { get; set; }
		public DateTime examDate { get; set; }
	}
}

