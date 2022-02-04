function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

const cookieVal = getCookie("cartItem");
let productIds = cookieVal !== "" ? cookieVal.split("-") : [];


$(".btn-add-cart").click(function () {
    Swal.fire(
        'Good job!',
        'Product Added!',
        'success'
    )
    const productId = $(this).attr("pro-id");
    productIds.push(productId);
    setCookie("cartItem", productIds.join("-"), 3)
})

$(".plus-btn").on("click", function () {
    let totalPrice = 0;
    const inputVal = Number($(this).parent().find("input").val()) + 1
    if (inputVal !== 1) {
        $(".minus-btn").css({ "pointer-events": "auto" })
    }
    const productId = $(this).parent().attr("pro-id");
    productIds = productIds.filter(c => c !== productId)
    for (let i = 0; i < inputVal; i++) {
        productIds.push(productId)
    }
    const price = Number($(this).parent().attr("pro-price"))
    $(this).parents("tr").find(".subtotal-amount").text("$" + price * inputVal)

    setCookie("cartItem", productIds.join("-"), 1)
    let subTotal = $(".cart-table .subtotal-amount").text().split("$")
    for (let i = 1; i < subTotal.length; i++) {
        totalPrice += Number(subTotal[i])
    }
    $(".cart-totals ul li span b").html(`$${totalPrice}`)
})



$(".minus-btn").on("click", function () {
    let totalPrice = 0;

    const inputVal = Number($(this).parent().find("input").val()) - 1

    if (inputVal === 1) {
        $(this).css({ "pointer-events": "none" })
    }

    const productId = $(this).parent().attr("pro-id");
    productIds = productIds.filter(c => c !== productId)
    for (let i = 0; i < inputVal; i++) {
        productIds.push(productId)
    }
    const price = Number($(this).parent().attr("pro-price"))
    $(this).parents("tr").find(".subtotal-amount").text("$" + price * inputVal)
    setCookie("cartItem", productIds.join("-"), 1)
    let subTotal = $(".cart-table .subtotal-amount").text().split("$")
    for (let i = 1; i < subTotal.length; i++) {
        totalPrice += Number(subTotal[i])
    }
    $(".cart-totals ul li span b").html(`$${totalPrice}`)
})

$(".product-subtotal .remove").on("click", function (e) {
    e.preventDefault()
    let totalPrice = 0;
    const productId = $(this).attr("pro-id");
    productIds = productIds.filter(p => p !== productId)
    setCookie("cartItem", productIds.join("-"), 1)
    $(this).parents("tr").remove();

    let subTotal = $(".cart-table .subtotal-amount").text().split("$")
    for (let i = 1; i < subTotal.length; i++) {
        totalPrice += Number(subTotal[i])
    }
    $(".cart-totals ul li span b").html(`$${totalPrice}`)
    if ($(".cart-area table tbody tr").length ==0) {
        
        $(".cart-area .my-cart-area").html('<p class="alert alert-danger">Kartda Məhsul tapılmadı</p>')
    }
})


