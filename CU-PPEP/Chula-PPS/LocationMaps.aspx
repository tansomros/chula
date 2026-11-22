<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LocationMaps.aspx.vb" Inherits=".LocationMaps" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="http://code.jquery.com/jquery-latest.min.js"></script>
    <script async defer
            src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCwjs6-YQxdxIJvdUkOV7RFTpACE4dcH-E&callback=initMap">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id='map' style='width:100%; height:700px; '></div>

    <script>
        var jsonObj = <%=json%>

        function initMap() {
            var mapOptions = {
                center: { lat: 16.75, lng: 100.5 },
                zoom: 7,
            }

            var maps = new google.maps.Map(document.getElementById("map"), mapOptions);

            var marker, info;

            $.each(jsonObj, function (i, item) {

                marker = new google.maps.Marker({
                    position: new google.maps.LatLng(item.lat, item.lng),
                    map: maps,
                    title: item.name
                });

                info = new google.maps.InfoWindow();

                google.maps.event.addListener(marker, 'click', (function (marker, i) {
                    return function () {
                        info.setContent(item.name);
                        info.open(maps, marker);
                    }
                })(marker, i));

            });

        }
    </script>
     
 
</asp:Content>
