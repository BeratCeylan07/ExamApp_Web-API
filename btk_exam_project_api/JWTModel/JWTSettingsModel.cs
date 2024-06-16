using System;
namespace btk_exam_project_api.JWTModel
{
	public class JWTSettingsModel
	{
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}

