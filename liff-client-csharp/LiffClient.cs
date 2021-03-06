﻿using LineDC.Liff.Data;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace LineDC.Liff
{
    public class LiffClient : ILiffClient
    {
        protected IJSRuntime JSRuntime { get; set; }
        protected string LiffId { get; set; }

        public bool Initialized { get; set; }

        public LiffClient(string liffId = null)
        {
            LiffId = liffId;
        }

        public async ValueTask Init(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
            await JSRuntime.InvokeVoidAsync("liff.init", new { liffId = LiffId }).ConfigureAwait(false);
        }

        public async ValueTask<string> GetOS()
        {
            return await JSRuntime.InvokeAsync<string>("liff.getOS").ConfigureAwait(false);
        }

        public async ValueTask<string> GetLanguage()
        {
            return await JSRuntime.InvokeAsync<string>("liff.getLanguage").ConfigureAwait(false);
        }

        public async ValueTask<string> GetVersion()
        {
            return await JSRuntime.InvokeAsync<string>("liff.getVersion").ConfigureAwait(false);
        }

        public async ValueTask<bool> IsInClient()
        {
            return await JSRuntime.InvokeAsync<bool>("liff.isInClient").ConfigureAwait(false);
        }

        public async ValueTask<bool> IsLoggedIn()
        {
            return await JSRuntime.InvokeAsync<bool>("liff.isLoggedIn").ConfigureAwait(false);
        }

        public async ValueTask Login(string redirectUri = null)
        {
            await JSRuntime.InvokeVoidAsync("liff.login", new { redirectUri }).ConfigureAwait(false);
        }

        public async ValueTask Logout()
        {
            await JSRuntime.InvokeVoidAsync("liff.logout").ConfigureAwait(false);
        }

        public async ValueTask<string> GetAccessToken()
        {
            return await JSRuntime.InvokeAsync<string>("liff.getAccessToken").ConfigureAwait(false);
        }

        public async ValueTask<LiffContext> GetContext()
        {
            var json = await JSRuntime.InvokeAsync<object>("liff.getContext").ConfigureAwait(false);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            return JsonSerializer.Deserialize<LiffContext>(json.ToString(), options);
        }

        public async ValueTask<IdTokenPayload> GetDecodedIDToken()
        {
            return await JSRuntime.InvokeAsync<IdTokenPayload>("liff.getDecodedIDToken").ConfigureAwait(false);
        }

        public async ValueTask<Profile> GetProfile()
        {
            return await JSRuntime.InvokeAsync<Profile>("liff.getProfile").ConfigureAwait(false);
        }

        public async ValueTask SendMessages(params object[] messages)
        {
            await JSRuntime.InvokeVoidAsync("liff.sendMessages", args: new[] { messages }).ConfigureAwait(false);
        }

        public async ValueTask OpenWindow(string url, bool external = false)
        {
            await JSRuntime.InvokeVoidAsync("liff.openWindow", new { url, external }).ConfigureAwait(false);
        }

        public async ValueTask ShareTargetPicker(params object[] messages)
        {
            await JSRuntime.InvokeVoidAsync("liff.shareTargetPicker", args: new[] { messages }).ConfigureAwait(false);
        }

        public async ValueTask<string> ScanCode()
        {
            return await JSRuntime.InvokeAsync<string>("liff.scanCode").ConfigureAwait(false);
        }

        public async ValueTask CloseWindow()
        {
            await JSRuntime.InvokeVoidAsync("liff.closeWindow").ConfigureAwait(false);
        }

        public async ValueTask<string> GetIDToken()
        {
            return await JSRuntime.InvokeAsync<string>("liff.getIDToken").ConfigureAwait(false);
        }

        public async ValueTask<string> GetLineVersion()
        {
            return await JSRuntime.InvokeAsync<string>("liff.getLineVersion").ConfigureAwait(false);
        }

        public async ValueTask<bool> IsApiAvailable(string apiName)
        {
            return await JSRuntime.InvokeAsync<bool>("liff.isApiAvailable", apiName).ConfigureAwait(false);
        }

        public async ValueTask<Friendship> GetFriendship()
        {
            return await JSRuntime.InvokeAsync<Friendship>("liff.getFriendship").ConfigureAwait(false);
        }
    }
}
