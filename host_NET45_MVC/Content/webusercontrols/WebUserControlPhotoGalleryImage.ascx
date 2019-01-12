<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WebUserControlPhotoGalleryImage.ascx.vb" Inherits="Content_webusercontrols_WebUserControlUserPhoto" %>

    <a href='<%= PhotoLink%>' title='<%= TitleName%>' data-gallery="">
        <img src='<%= ThumbnailsLink%>' >
    </a>
