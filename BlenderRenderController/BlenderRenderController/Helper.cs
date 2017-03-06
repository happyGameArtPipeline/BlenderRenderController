﻿using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace BlenderRenderController
{
    static class Helper
    {
        /* public static IEnumerable<string> AlphanumericSort(this IEnumerable<string> me)
         {
             string[] Separator = new string[] { "-" };
         }*/
        static public void clearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                clearFolder(di.FullName);
                di.Delete();
            }
        }
        static public void showErrors(List<string> errorCodes, MessageBoxIcon icon = MessageBoxIcon.Asterisk, string arg1 = "")
        {
            var errorText = "";
            foreach (var errorCode in errorCodes)
            {
                if(errorCode == AppErrorCodes.BLENDER_PATH_NOT_SET)
                {
                    errorText += "Please set correct path to Blender (blender.exe).\n";
                }
                if (errorCode == AppErrorCodes.FFMPEG_PATH_NOT_SET)
                {
                    errorText += "Please set correct path to FFmpeg (ffmpeg.exe).\n";
                }
                if (errorCode == AppErrorCodes.BLEND_FILE_NOT_EXISTS)
                {
                    errorText += "File does not exists anymore.\n";
                    errorText += "It was removed from the list of recent blends.\n";
                }
                if (errorCode == AppErrorCodes.RENDER_FORMAT_IS_IMAGE)
                {
                    errorText += "The render format is " + arg1 + " image.\n";
                    errorText += "You can render an image sequence with this tool but you will need to make a video with other SW.\n";
                }
                if (errorCode == AppErrorCodes.BLEND_OUTPUT_INVALID)
                {
                    errorText += "Could not resolve project's output path";
                }
            }
            MessageBox.Show(
                    errorText,
                    "",
                    MessageBoxButtons.OK,
                    icon);
        }
        static public string fixPath(string path)
        {
            var fixedPath = path.Trim().TrimEnd('\\');
            return fixedPath;
        }
    }
}
