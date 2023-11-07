# Demo
https://burger-masteres-app.azurewebsites.net

# BurgerMasters Documentation 
<pre>
  The BurgerMasters App is a dynamic online store catering to clients seeking effortless food delivery.
  This store empowers customers to curate their ideal orders by seamlessly adding or removing items to/from their carts
  Additionally, clients enjoy the convenience of revisiting their previous orders.

 For administrators, the BurgerMasters App grants comprehensive control. Admins possess the authority to create, edit, and delete products.
 A dedicated page is reserved for showcasing products exclusively crafted by the admin. 
 In addition, administrators are equipped with the ability to manage orders seamlessly. They can effortlessly accept, reject, or revoke order acceptances.
 </pre>

# Product Documentation
### Application Flow
    When the user starts the app he's redirected to the home page.
<p>
    <img height="300em" src="https://imageupload.io/ib/fiWYPWckVmp1nTk_1691675530.jpg" alt="homePage.jpg"/>
    <img height="300em" src="https://imageupload.io/ib/gJRaMLFSjRgXF2U_1691675754.jpg" alt="footer.jpg"/>
</p>

<pre>
    The three buttons on the right side of the navbar are:
    1. The home button leads the user to the home page if not logged.
    2. The login button leads to the login page.
    3. The register button leads to the register page.
</pre>
<p>
     <img src="https://imageupload.io/ib/gX3BItETYOjZRGo_1691676772.jpg" alt="guestButtons.jpg"/>
</p>

<pre>
    The login form asks for the user's email and password. 
    Validation for the input fields is implemented.
</pre

<p>
     <img height="300em" src="https://imageupload.io/ib/4BMbRHM9wiB3TbS_1691677022.jpg" alt="registrationForm.jpg"/>
     
     <img height="300em"  src="https://imageupload.io/ib/YJCLqPmbA0NSvi4_1691677434.jpg" alt="loginForm.jpg"/><
</p>

 <pre>
    The register form asks for 
        - Username
        - Email
        - Address
        - Birthdate,
        - Password
        - Confirm password.
    It also has validations.
 </pre>
 
 <pre>
    Upon entering an incorrect password, the login form for registered users 
    displays an error validation message.
 </pre>
 
 <p>
    <img height="300em" src="https://imageupload.io/ib/575UmhiEutjqHVU_1691677935.jpg" alt="loginFormError.jpg"/>
 </p>
 
  <pre>
    Upon successful login user is redirected to the menu page 
    with updated navigation bar. 
  </pre
  <p>
    <img src="https://imageupload.io/ib/42DTg5uZoE39fND_1691681227.jpg" alt="userNav.jpg"/>
  </p>
 
 <pre>
    The menu has 
        - Burgers
        - Sandwiches
        - Fries
        - Drinks
        - Hot-dogs
        - Grills
        - Salads
    Every menu page has search and sort by price ascending/descending order,
    portion size, name.
 </pre>
 
 <p>
    <img height="300em"  src="https://imageupload.io/ib/JI4etjVfFk4tVX5_1692131061.jpg" alt="menu.jpg"/>
 </p>
 
 <pre>
     When a user adds an item to their cart from the menu page, a side cart popup appears.
 </pre>
 <p>
    <img height="300em" src="https://imageupload.io/ib/9HunVq2WajEw034_1692131293.jpg" alt="sideCart.jpg"/>
 </p>
 
 
<pre>
    Clicking on "Cart" in the navigation bar or using the side cart
    checkout button opens a cart page for the product, where you can
    view information about each product and the total order price.
</pre>
 
<p>
    <img height="300em" src="https://imageupload.io/ib/1rxZbl9gjYJFuns_1692132147.jpg" alt="cart.jpg"/>
</p>
 
 
<pre>
    After placing an order, the cart is emptied, and you are redirected to the 
    "My Orders" page, where you can view information about your previous orders.
</pre>
 
<p>
    <img height="300em" src="https://imageupload.io/ib/1q1pXyHAMrNfpg5_1692132398.jpg" alt="myorders.jpg"/>
    <img height="300em" src="https://imageupload.io/ib/NhEm2aMPbcInywO_1692132634.jpg" alt="orderDetails.jpg"/>
</p>

<pre>
    Clicking on "Review" opens a page where you can share your opinion about the restaurant. 
    If you are the creator of the comment or an administrator, you have the ability to delete it. 
    Review comments utilize SignalR for two-way communication.
</pre>
 
<p>
    <img src="https://imageupload.io/ib/Yjz7ZPFcz7lU7kf_1692133154.jpg" alt="review.jpg"/>
</p>
 
 
<pre>
    When the admin logs in, their navigation bar appears as follows. 
    For testing purposes, the admin has the same permissions as a normal user.
</pre>
 
<p>
    <img src="https://imageupload.io/ib/JvWnGRBWaxelHpP_1692133254.jpg" alt="adminNav.jpg"/>
</p>

<pre>
    The admin has the ability to create new products.
</pre>

<p>
  <img height="300em" src="https://imageupload.io/ib/nyG0CsdX8WJdp1g_1692134656.jpg" alt="create.jpg"/>
</p>

<pre>
    The admin has a page called "MyPosts", which is identical to 
    the menu page, but it is exclusively meant for editing and deleting products.
</pre>
 
<p>
    <img height="300em" src="https://imageupload.io/ib/t31qCf1KMHKnXUP_1692133773.jpg" alt="myPosts.jpg"/>
    <img height="300em" src="https://imageupload.io/ib/QyVo5OtqNiUUzJy_1692133820.jpg" alt="myPostsDetails.jpg"/>
    <img height="300em" src="https://imageupload.io/ib/T7X7GLfckxI47Fd_1692134005.png" alt="edit.png"/>
    <img height="300em" src="https://imageupload.io/ib/R2SpKb9QPAj0q47_1692134093.jpg" alt="delete.jpg"/>
</p>

<pre>
    The admin has the capability to accept or decline orders from clients.
</pre>
 
<p>
    <img height="300em" src="https://imageupload.io/ib/QLOV90PDYxGRJpQ_1692134343.jpg" alt="orders.jpg"/>
</p>

<pre>
    If an order is mistakenly accepted, the admin can undo the acceptance, 
    which will return the order to the order page. 
</pre>
 
<p>
    <img  src="https://imageupload.io/ib/wpRBGcOqwB3fkQ9_1692134556.jpg" alt="history.jpg"/>
</p>

## Database Design

<p>
    <img  src="https://imageupload.io/ib/iHbfgWLMSLKY4nb_1692135313.jpg" alt="database (2).jpg"/>
</p>

## Tech Stack:

### API
<p></p>
<ul>
  <li>ASP.Net Core 6.0</li>
  <li>EntityFramework Core 6.0.1</li>
  <li>AutoMapper 12.0</li>
  <li>Swashbuckle.AspNetCore.Swagger 6.4</li>
  <li>Microsoft.AspNetCore.Identity 6.0.1</li>
  <li>Microsoft.AspNetCore.Authentication.JwtBearer 6.0.9</li>
</ul>

### Front-End
<p></p>
<ul>
  <li>React</li>
  <li>HTML</li>
  <li>CSS</li>
</ul>

### Database
<p></p>
<ul>
  <li>MSSQL Server</li>
</ul>

### Tests
<p></p>
<ul>
  <li>NUnit 3.13.3</li>
  <li>NUnit3TestAdapter 4.3.1</li>
  <li>Moq 4.18.2</li>
  <li>Microsoft.EntityFrameworkCore.InMemory 6.0.11</li>
</ul>

### Git tools
<p></p>
<ul>
  <li>GitHub</li>
  <li>GitHub Desktop</li>
</ul>
