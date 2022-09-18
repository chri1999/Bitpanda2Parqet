# Bitpanda2Parqet
Load Data from Bitpanda Api and make it uploadable for Parqet

This Tool is used to upload your Bitpanda Transactions directly to Parqet or export a .csv file from your Bitpanda API. The created .csv can then be uploaded to Parqet.

Notice as this project is in a very early stage: As I personally only have buy orders, the program will not get everything right and you should double check in Parqet. Please let me know if there is any data loaded wrong in Parqet. Furthermore, use the direct sync to parqet only with a test account, otherwise you could have lots of wrong transactions to deal with. It is recommended to export a .csv file, check for correctness and then upload it manually to parqet.

Here are some pictures:

![](https://github.com/chri1999/Bitpanda2Parqet/blob/main/pictures/MainView.PNG)
![](https://github.com/chri1999/Bitpanda2Parqet/blob/main/pictures/LoadedBitpandaData.PNG)
![](https://github.com/chri1999/Bitpanda2Parqet/blob/main/pictures/LoadedParqetData.PNG)

# Parameters and Description

You have to enter some Parameters into the tool. The Bitpanda API is always necessary, you will get it from your Bitpanda Account. Filename and Filepath should not be a problem, you will need this for the creation of a .csv file. For the direct upload to Parqet, you need the account number, which is shown in the URL when you open Parqet in your Browser and sign in.

![Account Number](https://github.com/chri1999/Bitpanda2Parqet/blob/main/pictures/Parqet%20URL.PNG)


Firefox Browser (similar in others):

To get the Parqet Token you have to be signed in to your Account and be in the Parqet Dashboard, then open developer tools by pressing F12 on your keyboard. Go to the marked points in the picture (Network & XHR) and press F5 to refresh the website. After that you should see some entries and look for the one like in the picture below. Go to response and copy the shown accessToken into Bitpanda2Parqet. You can also save the parameters you have entered into Bitpanda2Parqet by pressing the "Save Entries" Button. Those parameters will then be loaded at the start of the program or by pressing "Load saved Parameters".

![](https://github.com/chri1999/Bitpanda2Parqet/blob/main/pictures/Parqet%20Token.PNG)


