
let create_ac;
let email;
let password;
let confirm_password;
let Array_local = [];

document.addEventListener("DOMContentLoaded", function () {
    //  بتاكد من انو في عناصر داخلهم
    create_ac = document.getElementById("create");
    email = document.getElementById("email");
    password = document.getElementById("password");
    confirm_password = document.getElementById("confirm_password");
    //
    Array_local = JSON.parse(localStorage.getItem("users")) || [];

    if (create_ac) { 
        create_ac.addEventListener("click", function (event) {
            event.preventDefault(); // منع الصفحة من التحميل كمان مرة
            // بتاكد انو اليوزر دخل جميع الحقول
            if (email.value === "" || password.value === "" || confirm_password.value === "") {
                alert("الرجاء إدخال جميع الحقول!");
                return;
            }

            // بتاكد من انو الباسورد متطابق
            if (password.value === confirm_password.value) {
                // بشيك على email انو مش موجود في اللوكل
                let emailExists = Array_local.some(user => user.email === email.value);
                if (emailExists) {
                    alert("You cannot use this email.");
                } else {
                    let user = {
                        email: email.value,
                        password: password.value
                    };

                    Array_local.push(user);
                    // تخزين الarr  in localStorage
                    localStorage.setItem("users", JSON.stringify(Array_local));
                    alert("Account created successfully");
                    window.location.href = "sign-in.html"; //بودي اليوزر على صفحة sin in
                    console.log(Array_local); //  
                }
            } else {
                alert("The passwords do not match");
            }
        });
    } else {
        console.log("The 'create' button was not found.");
    }});
    let email_in = document.getElementById("email_in");
    console.log(email_in);
    