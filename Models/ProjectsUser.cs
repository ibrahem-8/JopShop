using System;
//created at 5
namespace JopShop.Models
{
    public class ProjectsUser
    {
        public int Id { get; set; }

        public string Title { get; set; }         // اسم المشروع
        public string Description { get; set; }   // وصف المشروع
        public string Path { get; set; }          // مسار الملف داخل wwwroot/projects
        public string FileName { get; set; }      // اسم الملف الفعلي
        public DateTime CreatedAt { get; set; }   // تاريخ الإنشاء
        public int UserId { get; set; }           // المفتاح الأجنبي للمستخدم
        public User User { get; set; }            // العلاقة مع جدول المستخدمين
    }
}
