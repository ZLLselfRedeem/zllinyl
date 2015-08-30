using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class Thumbnail
    {
        public Thumbnail(string id, byte[] data, byte[] bmaxData = null)
        {
            this.ID = id;
            this.Data = data;
            if (bmaxData != null)
                this.MaxData = bmaxData;
        }


        private string id;
        public string ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        private byte[] thumbnail_data;
        public byte[] Data
        {
            get
            {
                return this.thumbnail_data;
            }
            set
            {
                this.thumbnail_data = value;
            }
        }

        //原图数据 批量传图用 
        private byte[] max_data;
        public byte[] MaxData
        {
            get { return this.max_data; }
            set { this.max_data = value; }
        }
    }
}
