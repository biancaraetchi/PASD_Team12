import './css_pt_website.css'

function App() {
  function setLocationToGroningen(){
    document.getElementsByClassName("toggle")[0].style.display = "block"
    document.getElementById("partner").style.display = "none"
  }
  function setLocationToNotGroningen(){
    document.getElementsByClassName("toggle")[0].style.display = "none"
    document.getElementById("partner").style.display = "inline"
  }
  function submitForm(e){
    e.preventDefault()
  }
  return (
<>
  <section id="home">
          <h1>Disruptive Delivery: where our delivery disrupts your day! :)</h1>
        </section>
        <section id="contact">
           <h4>Enter your details to proceed to payment:</h4>
           <form action="" method="post" onSubmit={(e) => submitForm(e)}>
           <fieldset style={{width:"30%", display: "inline"}}>
             <legend>Is your delivery address located in Groningen?</legend>
             <div>
                 <input type="radio" id="location_yes" name="drone" value="huey" onClick={setLocationToGroningen}/>
               <label htmlFor="location_yes" style={{display:"inline"}}>Yes</label>              </div>
               <div>
                <input type="radio" id="location_no" name="drone" value="huey" defaultChecked onClick={setLocationToNotGroningen}/>
                <label htmlFor="location_no" style={{display:"inline"}}>No</label>
              </div>
            </fieldset>
            <br/>
             <div className="toggle" style={{width:"65%"}}>
               <label htmlFor="name">Full name:</label>
               <input type="text" id="name" name="name">
             </input>
               <br/>
               <label htmlFor="email">Email:</label>
               <input type="email" id="email" name="email">
             </input>
                
                <br/>
               <label htmlFor="address">Address:</label>
               <input type="text" id="address" name="address">
             </input>
                
                <br/>
               <label htmlFor="dimension">Dimensions of the packet:</label>
               <input type="text" id="dimension" name="dimension">
             </input>
                
                <br/>
               <label htmlFor="message">Additional notes for delivery driver:</label>
               <textarea id="message" name="message"></textarea><br/>
               <input type="submit" value="Go to payment page" id="submit"></input>
             </div>
             <input type="submit" id="partner" name="partner" value="Proceed to partner"></input>
  </form>
</section>

    <footer>Copyright ©2022 Disruptive Delivery</footer>
  {/* </body> */}
  </>
  );
}

export default App;
