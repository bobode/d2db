# d2db
Online Inventory viewing for d2bs.

Sign up for an account at http://bobode.dlinkddns.com, load the provided script in d2bs to upload account items.

I saved the script in /tools/UploadAccount.js

then call it anywhere with
load("tools/UploadAccount.js")  

I tested it in default after the game ready checks
that will load the script as a separate thread or task and not affect other scripts if there's network delays.

could also include / call it from default before quit if you wanted to ensure it updates with the latest information. 
Im pretty sure if you load right before quit() the script could get killed before it sends.


sample: http://bobode.dlinkddns.com/Home/ViewUser/c9a860ac-cba5-4eaa-b760-36214355393b
