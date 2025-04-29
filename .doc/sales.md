# Sales API

## Endpoints

### **GET /sales**
- **Description**: Retrieve a list of all sales
- **Query Parameters**:
  - `_page` (optional): Page number for pagination (default: 1)
  - `_size` (optional): Number of items per page (default: 10)
  - `_order` (optional): Ordering of results (e.g., "date desc, totalAmount asc")
- **Response**:
{
  "data": [
    {
      "id": "string",
      "saleNumber": "string",
      "date": "string",
      "customer": "string",
      "branch": "string",
      "totalAmount": "number",
      "isCancelled": "boolean",
      "items": [
        {
          "productId": "string",
          "quantity": "integer",
          "unitPrice": "number",
          "discount": "number",
          "totalAmount": "number"
        }
      ]
    }
  ],
  "totalItems": "integer",
  "currentPage": "integer",
  "totalPages": "integer"
}

---

### **POST /sales**
- **Description**: Add a new sale
- **Request Body**:
{
  "saleNumber": "string",
  "date": "string",
  "customer": "string",
  "branch": "string",
  "items": [
    {
      "productId": "string",
      "quantity": "integer",
      "unitPrice": "number"
    }
  ]
}



- **Response**:
{
  "id": "string",
  "saleNumber": "string",
  "date": "string",
  "customer": "string",
  "branch": "string",
  "totalAmount": "number",
  "isCancelled": "boolean",
  "items": [
    {
      "productId": "string",
      "quantity": "integer",
      "unitPrice": "number",
      "discount": "number",
      "totalAmount": "number"
    }
  ]
}

---

### **GET /sales/{id}**
- **Description**: Retrieve a specific sale by ID
- **Path Parameters**:
  - `id`: Sale ID
- **Response**:

{
  "id": "string",
  "saleNumber": "string",
  "date": "string",
  "customer": "string",
  "branch": "string",
  "totalAmount": "number",
  "isCancelled": "boolean",
  "items": [
    {
      "productId": "string",
      "quantity": "integer",
      "unitPrice": "number",
      "discount": "number",
      "totalAmount": "number"
    }
  ]
}




---

### **PUT /sales/{id}**
- **Description**: Update a specific sale
- **Path Parameters**:
  - `id`: Sale ID
- **Request Body**:
{
  "saleNumber": "string",
  "date": "string",
  "customer": "string",
  "branch": "string",
  "items": [
    {
      "productId": "string",
      "quantity": "integer",
      "unitPrice": "number"
    }
  ]
}

- **Response**:
{
  "id": "string",
  "saleNumber": "string",
  "date": "string",
  "customer": "string",
  "branch": "string",
  "totalAmount": "number",
  "isCancelled": "boolean",
  "items": [
    {
      "productId": "string",
      "quantity": "integer",
      "unitPrice": "number",
      "discount": "number",
      "totalAmount": "number"
    }
  ]
}




---

### **DELETE /sales/{id}**
- **Description**: Delete (or cancel) a specific sale
- **Path Parameters**:
  - `id`: Sale ID
- **Response**:
{
  "message": "Sale successfully cancelled"
}




---

### **GET /sales/customer/{customerId}**
- **Description**: Retrieve all sales for a specific customer
- **Path Parameters**:
  - `customerId`: Customer ID
- **Query Parameters**:
  - `_page` (optional): Page number for pagination (default: 1)
  - `_size` (optional): Number of items per page (default: 10)
  - `_order` (optional): Ordering of results (e.g., "date desc, totalAmount asc")
- **Response**:
{
  "data": [
    {
      "id": "string",
      "saleNumber": "string",
      "date": "string",
      "branch": "string",
      "totalAmount": "number",
      "isCancelled": "boolean"
    }
  ],
  "totalItems": "integer",
  "currentPage": "integer",
  "totalPages": "integer"
}



---

## Notes
- **Discounts and Business Rules**:
  - Discounts based on quantity (10% for 4+ items, 20% for 10-20 items) are calculated on the backend and reflected in the `discount` field of each item.
  - Sales with more than 20 items of the same product are rejected with a validation error.

- **Pagination and Ordering**:
  - Responses for list endpoints include `data`, `totalItems`, `currentPage`, and `totalPages`.

- **Cancellation**:
  - Cancelling a sale updates the `isCancelled` field to `true`.

---

This document provides a clear structure for implementing and documenting the Sales API. Let me know if you need further adjustments or additional details!
