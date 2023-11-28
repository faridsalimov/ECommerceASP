// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener("DOMContentLoaded", function () {
    console.log("DOM Loaded");
    document.querySelector('#form1').addEventListener('input', function () {
        var query = document.getElementById('form1').value;
        if (query.trim() != '') {
            fetch('/Product/Search?word=' + query)
                .then(function (response) {
                    if (!response.ok) {
                        return;
                    }
                    return response.json();
                })
                .then(function (data) {

                    console.log(data.length);
                    let content = "";
                    for (let i = 0; i < data.length; i++) {
                        const element = data[i];
                        //  console.log(element);
                        content += `
                         <tr>
                    <td>
                        ${data[i].productName}
                    </td>
                    <td>
                     ${data[i].unitPrice}
                    </td>
                    <td>
                    ${data[i].unitsInStock}
                    </td>
                    <td>
                        <a class="btn btn-xs btn-success"
                           href="/Cart/AddToCart?productId=${data[i].productId}&page=1&category=1">Add To Cart</a>


                        ${data[i].hasAdded ? `<a class="btn btn-xs btn-danger" href="/Cart/RemoveFirst?productId=${data[i].productId}">Remove</a>` : ``}
}
                    </td>
                </tr>
                        `;
                    }
                    document.querySelector('#mytable').innerHTML = content;
                    //document.innerHTML = data;
                })
                .catch(function (error) {
                    console.error('Error:', error);
                });
        }
    });
})