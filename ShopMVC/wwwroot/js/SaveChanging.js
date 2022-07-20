function updateValue(e) {
    var productId = e.id;
    var amount = e.value;
    if (e.value < 1) {
        e.value = "1";
        return
    }

    var data = {
        amount: amount,
        id: productId
    }
    addValue(data);
}

function addValue(amount) {
    var xmlhttp = new XMLHttpRequest();
    var url = "/Basket/ChangeProduct";
    xmlhttp.open("POST", url);
    xmlhttp.setRequestHeader("Content-type", "application/json");
    var value = amount;
    xmlhttp.send(JSON.stringify(value));
}