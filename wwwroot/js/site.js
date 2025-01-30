
function processPayment() {
    
    document.getElementById("animationContainer").style.display = "block";

    setTimeout(function () {
        // Сховати анімацію
        document.getElementById("animationContainer").style.display = "none";

        // Відправити форму
        document.getElementById("submitForm").submit();
        
    }, 2000);    

}
