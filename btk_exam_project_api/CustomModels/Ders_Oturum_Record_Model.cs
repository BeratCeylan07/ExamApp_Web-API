using btk_exam_project_api.Models;

namespace btk_exam_project_api.CustomModels
{
    public class Ders_Oturum_Record_Model
    {
        public int Id { get; set; }

        public string Uid { get; set; } = null!;

        public int Dersid { get; set; }

        public int Teacherid { get; set; }

        public DateTime Tarih { get; set; }

        public DateTime Baslangic { get; set; }

        public DateTime Bitis { get; set; }

        public int IsCreatedUserId { get; set; }
        public virtual Kullanicilar IsCreatedUser { get; set; } = null!;
        public DateTime IsCreatedDate { get; set; }

        public int IsModifiedUserId { get; set; }
        public virtual Kullanicilar IsModifiedUser { get; set; } = null!;
        public DateTime IsModifiedDate { get; set; }

        public bool IsActive { get; set; }

        public virtual Der Ders { get; set; } = null!;

        public virtual ICollection<DersOturumUserSet> DersOturumUserSets { get; set; } = new List<DersOturumUserSet>();

        public virtual Kullanicilar Teacher { get; set; } = null!;
    }
}
