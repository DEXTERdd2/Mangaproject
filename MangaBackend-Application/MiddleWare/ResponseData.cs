using MangaBackend_Application.Common.ResponseType;
using System.Net;

namespace ArcadeGameBackend.Application.MiddleWare
{
    public static class ResponseData
    {
        public static ResponseModel SaveResponse(dynamic model, string message = "Data has been Saved Successfully")
        {
            ResponseModel responseModel = new();
            responseModel.statusCode = 200;
            responseModel.ResponseMessage = message;
            responseModel.DataModel = model;
            responseModel.IsError = false;
            return responseModel;

        }
        public static ResponseModel ErrorResponse(dynamic model, string Message)
        {
            ResponseModel responseModel = new();
            responseModel.statusCode = (int)HttpStatusCode.BadRequest;
            responseModel.ResponseMessage = Message;
            responseModel.DataModel = model;
            responseModel.IsError = true;
            return responseModel;

        }
        public static ResponseModel FoundSuccessResponse(dynamic model)
        {
            ResponseModel responseModel = new();
            responseModel.statusCode = (int)HttpStatusCode.OK;
            responseModel.ResponseMessage = "";
            responseModel.DataModel = model;
            responseModel.IsError = false;
            return responseModel;

        }
        public static ResponseModel NotSuccessResponse(string Message)
        {
            ResponseModel responseModel = new();
            responseModel.statusCode = (int)HttpStatusCode.BadRequest;
            responseModel.ResponseMessage = Message;
            responseModel.DataModel = null;
            responseModel.IsError = true;
            return responseModel;

        }
        public static ResponseModel GetSuccessResponse(dynamic? model, dynamic Model2)
        {
            ResponseModel responseModel = new();
            if (model != null)
            {
                responseModel.statusCode = (int)HttpStatusCode.OK;
                responseModel.DataModel = model;
            }
            else
            {
                responseModel.statusCode = (int)HttpStatusCode.OK;
                responseModel.ResponseMessage = "No Data Found";
            }
            if (Model2 != null)
            {
                responseModel.statusCode = (int)HttpStatusCode.OK;
                responseModel.DataModel = Model2;
            }
            else
            {
                responseModel.statusCode = (int)HttpStatusCode.OK;
                responseModel.IsError = false;
            }
            return responseModel;

        }
        public static ResponseModel SaveResponse(string Message = "Record has been Saved Successfully")
        {
            ResponseModel responseModel = new();
            responseModel.statusCode = 200;
            responseModel.ResponseMessage = Message;
            responseModel.IsError = false;
            return responseModel;

        }
        public static ResponseModel GetSuccessResponse(dynamic? model, string Message = "")
        {
            ResponseModel responseModel = new();
            if (model != null)
            {
                responseModel.statusCode = (int)HttpStatusCode.OK;
                responseModel.DataModel = model;
            }
            else
            {
                responseModel.statusCode = (int)HttpStatusCode.OK;
                responseModel.ResponseMessage = "No Data Found";
            }
            if (Message.Length > 0)
            {
                responseModel.ResponseMessage = Message;
            }

            responseModel.IsError = false;

            return responseModel;
        }
        public static ResponseModel DeleteSuccessResponse(string ResponseMessage = "")
        {
            ResponseModel responseModel = new();
            responseModel.statusCode = (int)HttpStatusCode.OK;
            responseModel.ResponseMessage = "Data has been Deleted Successfully";
            responseModel.IsError = false;
            if (ResponseMessage.Length > 0)
            {
                responseModel.ResponseMessage = ResponseMessage;
            }
            return responseModel;
        }
        public static ResponseModel GetSuccessResponseStream(MemoryStream? model, string Message = "")
        {
            ResponseModel responseModel = new();
            if (model != null)
            {
                responseModel.statusCode = (int)HttpStatusCode.OK;
                responseModel.DataModel = model;
            }
            else
            {
                responseModel.statusCode = (int)HttpStatusCode.OK;
                responseModel.ResponseMessage = "No Data Found";
            }
            if (Message.Length > 0)
            {
                responseModel.ResponseMessage = Message;
            }

            responseModel.IsError = false;

            return responseModel;
        }

        public static ResponseModel ErrorResponse(string v)
        {
            throw new NotImplementedException();
        }
    }
}