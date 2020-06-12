using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ModelInfoService
    {
        private readonly ModelInfoDao modelInfoDao = new ModelInfoDao();

        /// <summary>
        /// 根據fileNo查詢模板文件信息
        /// </summary>
        /// <param name="fileNo"></param>
        /// <returns></returns>
        public ModelFile queryModelFileByNo(string fileNo)
        {
            return modelInfoDao.queryModelFileByNo(fileNo);
        }


        public ModelFile queryModelInfoNo(string fileNo)
        {
            return modelInfoDao.queryModelInfoNo(fileNo);
        }

        /// <summary>
        /// 模糊查詢模板文件信息
        /// </summary>
        /// <param name="fileNo"></param>
        /// <returns></returns>
        public List<ModelFile> queryModelFileList()
        {
            return modelInfoDao.queryModelFileList();
        }


        public string previewModelFile(string fileNo)
        {
            ModelFile modelInfo = modelInfoDao.queryModelInfoNo(fileNo);
            if(modelInfo != null)
            {
                string templateFile = System.IO.Directory.GetCurrentDirectory() + "\\" + modelInfo.Filename;
                if (File.Exists(templateFile))
                {
                    return templateFile;
                }
                else
                {
                    ModelFile modelFile = modelInfoDao.queryModelFileByNo(fileNo);
                    FileStream fs = new FileStream(templateFile, FileMode.CreateNew);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(modelFile.Fileaddress, 0, modelFile.Fileaddress.Length); //用文件流生成一个文件
                    bw.Close();
                    fs.Close();
                    bw.Dispose();
                    fs.Dispose();
                    return templateFile;
                }
            }else
            {
                return null;
            }

        }
    }
}
