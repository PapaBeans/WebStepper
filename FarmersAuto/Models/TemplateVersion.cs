using System;

namespace InsuranceAutomation.Models
{
    /// <summary>
    /// Represents a version of a template with timestamp information.
    /// </summary>
    public class TemplateVersion
    {
        /// <summary>
        /// Gets or sets the file name of this template version.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the full file path to this template version.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when this version was created.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets a formatted display text for this version.
        /// </summary>
        public string DisplayText => $"{Timestamp:yyyy-MM-dd HH:mm:ss} - {FileName}";

        /// <summary>
        /// Creates a TemplateVersion instance from a file name.
        /// </summary>
        /// <param name="fileName">The file name with timestamp embedded.</param>
        /// <param name="filePath">The full file path.</param>
        /// <returns>A TemplateVersion instance or null if parsing fails.</returns>
        public static TemplateVersion FromFileName(string fileName, string filePath)
        {
            try
            {
                // Extract timestamp from filename format: name_yyyyMMdd_HHmmss.json
                int lastUnderscore = fileName.LastIndexOf('_');
                if (lastUnderscore <= 0)
                    throw new FormatException("Invalid filename format");

                string fileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(fileName);
                string timestamp = fileNameWithoutExt.Substring(fileNameWithoutExt.LastIndexOf('_') + 1);
                
                DateTime versionDate = DateTime.ParseExact(timestamp, "yyyyMMdd_HHmmss", null);
                
                return new TemplateVersion
                {
                    FileName = fileName,
                    FilePath = filePath,
                    Timestamp = versionDate
                };
            }
            catch
            {
                // If parsing fails, return version with current time
                return new TemplateVersion
                {
                    FileName = fileName,
                    FilePath = filePath,
                    Timestamp = DateTime.Now
                };
            }
        }
    }
}
