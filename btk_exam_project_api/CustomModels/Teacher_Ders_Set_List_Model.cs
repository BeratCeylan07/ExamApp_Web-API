using System;
using btk_exam_project_api.Models;

namespace btk_exam_project_api.CustomModels
{
	public class Teacher_Ders_Set_List_Model
	{
		public string dersOturumUserSetUID { get; set; }
		public DateTime dersTarih { get; set; }
		public DateTime baslangic { get; set; }
		public DateTime bitis { get; set; }
		public string dersAd { get; set; }
        public string bilgi { get; set; }
        public int status { get; set; }
		public bool isActive { get; set; }
	}
}

