﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Stml.Infrastructure.IO
{
    /// <summary>
    /// A helper class for File operations.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Checks and deletes given file if it does exists.
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        public static void DeleteIfExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
