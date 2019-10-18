# SimpleWebApi
Product shop web api completed using ASP.NET

## Functional

### /shop <br/>
Methods GET: get all products from the shop <br/>
        POST: insert array of products into shop <br/>
        PATCH: add or update product <br/>

### /shop/{productName}
Methods GET: get product with specified product name <br/>
        DELETE: delete product with specified product name <br/>

### /shop/order
Method POST: make an order request <br/>

## Examples
POST: curl -X POST -H "Content-Type:application/json" -d '{"products":[{"Name":"Yogy","Price":65.60}]}' -i http://localhost:52455/ProductShopService.svc/shop <ba/>
OUTPUT: {"PostProductsResult":[{"Name":"Yogy","Price":65.60}]} <ba/>

GET: curl -X GET -H "Content-Type:application/json" -i http://localhost:52455/ProductShopService.svc/shop <ba/>
OUTPUT: [{"Name":"orange","Price":79.80},{"Name":"apple","Price":60},{"Name":"water","Price":39.90},{"Name":"butter","Price":56},{"Name":"ketchup","Price":59.9},{"Name":"ice cream","Price":58},{"Name":"cheese","Price":146.80},{"Name":"milk","Price":33},{"Name":"bread","Price":20.21},{"Name":"chocolate","Price":45.45},{"Name":"cookies","Price":120.55}] <ba/>

ORDER: curl -X POST -H "Content-Type:application/json" -d '{"orderRequest":{"CustomersName":"foo","RequestedProducts":[{"Name":"water", "RequestedNumber":2}, {"Name":"apple","RequestedNumber":4}]}}' -i http://localhost:52455/ProductShopService.svc/shop/order <ba/>
OUTPUT: {"MakeOrderResult":{"CustomersName":"foo","Date":"18.10.2019","Id":1,"Products":[{"Name":"water","Price":39.90,"ProductCount":2,"SumPrice":79.80},{"Name":"apple","Price":60,"ProductCount":4,"SumPrice":240}],"TotalPrice":319.80}} <ba/> 

## Possible troubles
May cause unauthorized access exception of file not found if IIS Express is located in system directory
Possible fix is using db instead of storing data in json file