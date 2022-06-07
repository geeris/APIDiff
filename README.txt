API contains two endpoints for recieving Base64 encoded binary data.

LeftController <host>/api/left/<ID>
RightController <host>/api/right/<ID>

Both endpoints have PUT method that is used to add new objects to JSON file if data is encoded. If certain object already exists, 
its data would be changed.
Endpoints should recieve JSON object through body containing data as well as ID from query string.



Third endpoint <host>/api/diff/<ID> is used to check differences between data of objects, of the given ID, in JSON file.
This endpoint contains GET method that recieves ID from the query string and gives result in JSON format.

Before checking differences, it is important to check if objects of the given id exist on both, Left and Right endpoints and contain data.

If condition is not met, endpoint returns NotFound response.

In case every check has been satisfied, we check whether data of objects:
	are equal - return message in JSON format
	are of different size - return message in JSON format
	are of the same size but not equal - returns message followed by array with offset(position of byte that differs) and its length representing 
	difference in data.



Code is covered with several unit tests.