using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filterware
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                // 記錄例外狀況
                _logger.LogError(context.Exception, "發生未處理的例外狀況");

                // 設定回應狀態碼
                context.HttpContext.Response.StatusCode = 500; // Internal Server Error

                // 設定回應內容類型
                context.HttpContext.Response.ContentType = "application/json";

                // 設定回應內容 (JSON 格式)
                var errorResponse = new
                {
                    Message = "發生未預期的錯誤，請稍後再試。",
                    Exception = context.Exception.InnerException != null ? context.Exception.InnerException.Message : context.Exception.Message // 注意：在生產環境中，不要直接將詳細的例外狀況訊息返回給用戶端
                };

                // 將錯誤回應寫入回應內容
                context.Result = new JsonResult(errorResponse);

                // 設定例外狀況已處理，避免預設的例外狀況處理邏輯執行
                context.ExceptionHandled = true;
            }
        }
    }
}