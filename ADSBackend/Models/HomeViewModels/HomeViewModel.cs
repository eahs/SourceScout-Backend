using ADSBackend.Models.Identity;

namespace ADSBackend.Models.HomeViewModels
{
    public class HomeViewModel
    {
        public ApplicationUser User { get; set; }
        // top of page banner
        <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        </asp:Content>
        <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <section>
            // Put in images here
        <img src="IMGs/TopofHomeBanner.jpg"/>
        </section>
        // middle section
        <section>
      <div class="container">
         <div class="row">
            <div class="col-12">
               <center>
                  <h2>Our Features</h2>
                  <p><b>Our 3 Primary Features -</b></p>
               </center>
            </div>
         </div>
         <div class="row">
            <div class="col-md-4">
               <center>
            // Put in images here
                  <img width="150px" src="IMGs/SourceScoutLogo.png"/>
                  <h4>Lesson Help!</h4>
                  <p class="text-justify">Do you need asistance with a school lesson? We are here to help!</p>
               </center>
            </div>
            <div class="col-md-4">
               <center>
            // Put in images here
                  <img width="150px" src="IMGs/SourceScoutLogo.png"/>
                  <h4>External Infomartion!</h4>
                  <p class="text-justify">Here at Source Scout we find all helpful material and put them together here! </p>
               </center>
            </div>
            <div class="col-md-4">
               <center>
            // Put in images here
                  <img width="150px" src="IMGs/SourceScoutLogo.png"/>
                  <h4>All Your Sources Together!</h4>
                  <p class="text-justify">You can login and save, look back, and even look at similar links to assist you on your academic journey!</p>
               </center>
            </div>
         </div>
      </div>
   </section>

    }
}
