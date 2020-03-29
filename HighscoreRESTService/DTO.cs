using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Minesweeper.Models;
using System.Runtime.Serialization;


namespace HighscoreRESTService
{
    [DataContract]
    public class DTO
    {
		public DTO(int ErrorCode, string ErrorMsg, List<HighscoreModel> Data)
		{
			this.ErrorCode = ErrorCode;
			this.ErrorMsg = ErrorMsg;
			this.Data = Data;
		}

		[DataMember]
		public int ErrorCode { get; set; }

		[DataMember]
		public string ErrorMsg { get; set; }

		[DataMember]
		public List<HighscoreModel> Data { get; set; }

	}
}