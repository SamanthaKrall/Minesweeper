using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace HighscoreRESTService
{
    [ServiceContract]
    public interface IHighscoreService
    {
		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHighscores/")]
		DTO GetHighscores();

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUserHighscore/{userName}")]
		DTO GetUserHighscore(string userName);

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTopThreeHighscores/")]
		DTO GetTopThree();
	}
}