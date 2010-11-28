<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Title="Untitled Page"  %>
<asp:Content ID="Content" Runat="Server" ContentPlaceHolderID="Container" >

<% var postAndQueryStringVariables = new NameValueCollection(Request.Form); postAndQueryStringVariables.Add(Request.QueryString); %>
<% if (postAndQueryStringVariables.Count > 0) { %>
<h2>Params</h2>
<dl id="variables">
  <% foreach (var variable in postAndQueryStringVariables.AllKeys) { %>
  <dt data-variable="<%= variable %>"><%= variable %>:</dt>
  <dd data-variable="<%= variable %>"><%= postAndQueryStringVariables[variable] %></dd>
  <% } %>
</dl>
<div style="clear: left;" />
<% } %>

<h1>Form</h1>
<fieldset>
  <legend>Fill out some stuff to POST</legend>
  <form method="post">
    <label for="DogName">Dog name</label>
    <input type="text" id="DogName" placeholder="Enter dog name here" value="<%= Request["DogName"] %>" autofocus="true" />

    <label for="DogBreed">Breed of dog</label>
    <input type="text" name="DogBreed" placeholder="eg. Golden Retriever" value="<%= Request["DogBreed"] %>" />

    <input type="submit" value="POST some stuff" />
  </form>
</fieldset>
</asp:Content>
