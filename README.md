## This is an e-commerce application that sells apparel using ASP.NET Web Forms

### Features
- Able to search clothes name
- Interactive Carrousel displaying images
- Shopping Cart & Stripe checkout
- 2 type of accounts, User & Admin
- Able to login with Google but not Facebook
- Admin CRUD for products (clothings)
- Customer are able to leave rating
- Support 2FA
- Account verification
- Password reset
- 3 Charts:
  - Which country did the customer register their account in
  - Revenue for last 3 months including current month
  - Bar char see which is the most popular category


### Limitations
- Unable to purchase same clothe but different size/colour - Only 1 type 
- There is no pagination for products 
- Customers are not able to view their purchase history
- There is no description, only overview
- There is no wishlist
- Newsletter is not implemented


## Before running this application

Inside App_Data 
- Create a database file named **Database1.mdf**
- Run script.sql, this creates all the tables, an admin account with email: `admin@email.com` and password: `admin` and a dummy row for most tables
- Run storedprocedure.sql, this creates the CRUD for products

Create a directory in `./assets/img/_clothing/carousel/00000000-0000-4000-A000-111111111111` <br>
All the images will be stored inside `./assets/img/_clothing/carousel/{productID}/` directory
