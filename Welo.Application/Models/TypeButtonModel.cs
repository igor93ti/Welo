using System;

namespace Welo.Application.Models
{


    /// <summary>
    ///  openUrl	URL to be opened in the built-in browser.
    ///  imBack Text of message which client will sent back to bot as ordinary chat message.All other participants will see that was posted to the bot and who posted this.
    ///  postBack Text of message which client will post to bot. Client applications will not display this message.
    ///  call Destination for a call in following format: "tel:123123123123"
    ///  playAudio playback audio container referenced by URL
    ///  playVideo   playback video container referenced by URL
    ///  showImage show image referenced by URL
    ///  downloadFile download file referenced by URL
    ///  signin OAuth flow URL
    /// </summary>
    [Serializable]
    public enum TypeButtonModel
    {
        ImBack,
        PostBack,
        Call,
        PlayAudio,
        PlayVideo,
        ShowImage,
        DownloadFile,
        Signin
    }
}