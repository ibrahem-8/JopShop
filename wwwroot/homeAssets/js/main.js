(function ($) {
    "use strict";

    // Spinner
    var spinner = function () {
        setTimeout(function () {
            if ($('#spinner').length > 0) {
                $('#spinner').removeClass('show');
            }
        }, 1);
    };
    spinner();
    
    
    // Initiate the wowjs
    new WOW().init();


    // Sticky Navbar
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.sticky-top').css('top', '0px');
        } else {
            $('.sticky-top').css('top', '-100px');
        }
    });
    
    
    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({scrollTop: 0}, 1500, 'easeInOutExpo');
        return false;
    });


    // Header carousel
    $(".header-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 1500,
        items: 1,
        dots: true,
        loop: true,
        nav : true,
        navText : [
            '<i class="bi bi-chevron-left"></i>',
            '<i class="bi bi-chevron-right"></i>'
        ]
    });


    // Testimonials carousel
    $(".testimonial-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 1000,
        center: true,
        margin: 24,
        dots: true,
        loop: true,
        nav : false,
        responsive: {
            0:{
                items:1
            },
            768:{
                items:2
            },
            992:{
                items:3
            }
        }
    });
    
})(jQuery);
/////////////////////////////////////
   const container = document.getElementById("postsContainer");
    const btnLeft = document.getElementById("scrollPostsLeft");
    const btnRight = document.getElementById("scrollPostsRight");

    btnLeft?.addEventListener("click", () => {
        container.scrollBy({ left: -container.offsetWidth, behavior: 'smooth' });
    });

    btnRight?.addEventListener("click", () => {
        container.scrollBy({ left: container.offsetWidth, behavior: 'smooth' });
    });
// function showMessage(message, type = 'error') {
//   const messageBox = document.getElementById('messageBox');
//   messageBox.innerText = message;

//   // تغيير الألوان حسب نوع الرسالة
//   if (type === 'success') {
//       messageBox.style.backgroundColor = '#d4edda';
//       messageBox.style.color = ' rgba(20, 108, 67, 0.902);';
//       messageBox.style.border = '1px solid #c3e6cb';
//   } else if (type === 'error') {
//       messageBox.style.backgroundColor = '#f8d7da';
//       messageBox.style.color = '#721c24';
//       messageBox.style.border = '1px solid #f5c6cb';
//   } else if (type === 'info') {
//       messageBox.style.backgroundColor = '#d1ecf1';
//       messageBox.style.color = '#0c5460';
//       messageBox.style.border = '1px solid #bee5eb';
//   }

//   messageBox.style.display = 'block';

//   // إخفاء الرسالة بعد 3 ثواني تلقائيًا
//   setTimeout(() => {
//       messageBox.style.display = 'none';
//   }, 3000);
// }

// /////////////////////////////////////////////////register1///////////////////
// document.addEventListener("DOMContentLoaded", function () {
//     const form = document.getElementById("registerForm");
//     const email = document.getElementById("email");
//     const password = document.getElementById("password");
//     const confirm_password = document.getElementById("confirm_password");

//     form.addEventListener("submit", function (e) {
//         if (!email.value || !password.value || !confirm_password.value) {
//             alert("Please fill in all fields.");
//             e.preventDefault();
//             return;
//         }

//         const regex = /^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[\W_]).{8,}$/;
//         if (!regex.test(password.value)) {
//             alert("The password must contain at least 8 characters, uppercase, lowercase, number, and special character.");
//             e.preventDefault();
//             return;
//         }

//         if (password.value !== confirm_password.value) {
//             alert("The passwords do not match.");
//             e.preventDefault();
//         }
//     });
// });


// /*
// document.addEventListener("DOMContentLoaded", function () {
//     const create_at = document.getElementById("create");
//     const email = document.getElementById("email");
//     const password = document.getElementById("password");
//     const confirm_password = document.getElementById("confirm_password");

//     // ✅ دالة التحقق من قوة كلمة المرور
//     function isValidPassword(pw) {
//         const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$/;
//         return regex.test(pw);
//     }

//     // ✅ دالة لعرض رسالة (بسيطة بـ alert مؤقتاً)
//     function showMessage(message, type) {
//         alert(`[${type.toUpperCase()}] ${message}`);
//     }

//     if (create_at) {
//         create_at.addEventListener("click", function (event) {
//             // منع إرسال النموذج مؤقتًا
//             event.preventDefault();

//             // التحقق من الحقول الفارغة
//             if (email.value === "" || password.value === "" || confirm_password.value === "") {
//                 showMessage("Please enter all fields", "info");
//                 return;
//             }

//             // التحقق من قوة الباسورد
//             if (!isValidPassword(password.value)) {
//                 showMessage("The password must contain at least 8 characters, an uppercase letter, a lowercase letter, a number, and a special character.", "error");
//                 return;
//             }

//             // التحقق من تطابق الباسورد
//             if (password.value !== confirm_password.value) {
//                 showMessage("The passwords do not match", "error");
//                 return;
//             }

//             // ✅ إذا كل شيء صحيح، إرسال النموذج
//             event.target.closest("form").submit();
//         });
//     }
// });*/
// /////////////// profile.html//////////////////

// document.addEventListener("DOMContentLoaded", function () {
//     const form = document.getElementById("profileForm");

//     if (!form) return;

//     form.addEventListener("submit", function (e) {
//         const fullName = document.getElementById("fname_pro").value.trim();
//         const userName = document.getElementById("user_name").value.trim();
//         const phone = document.getElementById("ph_pro").value.trim();
//         const gender = document.getElementById("select_pro").value.trim();
//         const cv = document.getElementById("cv_pro");

//         let errors = [];

//         if (!fullName) errors.push("Full Name is required.");
//         if (!userName) errors.push("Username is required.");
//         if (!phone.match(/^[0-9]{10}$/)) errors.push("Phone number must be 10 digits.");
//         if (!gender) errors.push("Gender is required.");
//         if (!cv || cv.files.length === 0) errors.push("Please upload your CV.");

//         if (errors.length > 0) {
//             alert(errors.join("\n"));
//             e.preventDefault();
//         }
//     });
// });

/*
document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("profileForm");

    form.addEventListener("submit", function (e) {
        const firstName = document.getElementById("fname_pro").value;
        const lastName = document.getElementById("lname_pro").value;
        const birthDate = document.getElementById("date_pro").value;
        const gender = document.getElementById("select_pro").value;
        const phoneNumber = document.getElementById("ph_pro").value;
        const userName = document.getElementById("user_name").value;
        const cv = document.getElementById("cv_pro");

        if (!firstName || !lastName || !birthDate || !gender || !phoneNumber || !userName || cv.files.length === 0) {
            alert("Please fill in all fields and upload a CV.");
            e.preventDefault();
        }
    });
});*/


//// ✅ Sign Up Page (sign_up.html)
//// ------------------------------
//let create_at;
//let email;
//let password;
//let confirm_password;
//let Array_local = [];

//document.addEventListener("DOMContentLoaded", function () {
//  create_at = document.getElementById("create");
//  email = document.getElementById("email");
//  password = document.getElementById("password");
//  confirm_password = document.getElementById("confirm_password");

//  Array_local = JSON.parse(localStorage.getItem("users")) || [];


//  function isValidPassword(pw) {
//    const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$/;
//    return regex.test(pw);
//  }

//  if (create_at) {
//    create_at.addEventListener("click", function (event) {
//      event.preventDefault();

//      if (email.value === "" || password.value === "" || confirm_password.value === "") {
//        showMessage("Please enter all fields", "info");
//        return;
//      }

//      if (!isValidPassword(password.value)) {
//        showMessage("The password must contain at least 8 characters, an uppercase letter, a lowercase letter, a number, and a special character.", "error");
//        return;
//      }

//      if (password.value !== confirm_password.value) {
//        showMessage("The passwords do not match", "error");
//        return;
//      }

//      const emailExists = Array_local.some(user => user.email === email.value);
//      if (emailExists) {
//        showMessage("You cannot use this email address.", "error");
//        return;
//      }

//      const user = {
//        email: email.value,
//        password: password.value
//      };
    
//      Array_local.push(user);
//      localStorage.setItem("users", JSON.stringify(Array_local));
//      localStorage.setItem("currentUser", email.value);

//      showMessage("Account created successfully", "success");
//      window.location.href = "profile.html";
//    })}
    
//});
  
//// ------------------------------
//// ✅ Login Page (login.html)
//// ------------------------------
//document.addEventListener("DOMContentLoaded", function () {
//  const container = document.getElementById("profile-container");
//  const params = new URLSearchParams(window.location.search); // الحصول على البيانات من الـ URL
//  const userEmail = params.get("email");

//  // عرض رسالة إذا لم يوجد بريد إلكتروني
//  if (!userEmail) {
//    container.innerHTML = `<p class="text-center">لا يوجد مستخدم لهذا البريد الإلكتروني.</p>`;
//    return;
//  }

//  // جلب بيانات المستخدم من localStorage باستخدام البريد الإلكتروني
//  const allUsers = JSON.parse(localStorage.getItem("users")) || [];
//  const user = allUsers.find(user => user.email === userEmail);

//  // عرض رسالة إذا لم يتم العثور على المستخدم
//  if (!user) {
//    container.innerHTML = `<p class="text-center">لا يوجد مستخدم بهذا البريد الإلكتروني.</p>`;
//    return;
//  }

//  // عرض تفاصيل الملف الشخصي للمستخدم
//  const profileHTML = `
//    <div class="profile-card p-4 shadow">
//      <h3>الملف الشخصي</h3>
//      <img src="${user.image || 'assets/img/default.png'}" alt="User Profile Image" class="img-fluid" style="height: 150px; object-fit: cover;">
//      <p><strong>الاسم:</strong> ${user.firstName} ${user.lastName}</p>
//      <p><strong>البريد الإلكتروني:</strong> ${user.email}</p>
//      <p><strong>حالة الحساب:</strong> ${user.isDisabled ? "موقوف" : "نشط"}</p>
//    </div>
//  `;

//  container.innerHTML = profileHTML;
//});

//document.addEventListener("DOMContentLoaded", function () {
//  let log_in = document.getElementById("login");
//  let email_log = document.getElementById("em_log");
//  let password_log = document.getElementById("pa_log");

//  if (log_in) {
//    log_in.addEventListener("click", function (event) {
//      event.preventDefault();

//      if (email_log.value === "" || password_log.value === "") {
//        showMessage("Please enter your email and password!" , 'success');
//        return;
//      }

//      let users = JSON.parse(localStorage.getItem("users")) || [];

//      let foundUser = users.find(user => user.email === email_log.value && user.password === password_log.value);

//      if (foundUser) {
//        if (foundUser.isDisabled) {
//          // إذا كان الحساب معطلًا، منع تسجيل الدخول
//          showMessage("Your account is disabled. You can't log in." ,'success');
//          return;
//        }

//        // حفظ المستخدم الحالي
//        localStorage.setItem("currentUser", email_log.value);

  
//        window.location.href = "index.html"; // التوجيه إلى الصفحة الرئيسية
//      } else {
//        showMessage("Incorrect email or password!",'error');
//      }
//    })
    
//  }});
  
//  // ------------------------------
////  Profile Page (profile.html)
//// ------------------------------

//function saveProfile() {
//  let firstName = document.getElementById("fname_pro").value;
//  let lastName = document.getElementById("lname_pro").value;
//  let birthDate = document.getElementById("date_pro").value;
//  let gender = document.getElementById("select_pro").value;
//  let phoneNumber = document.getElementById("ph_pro").value;
//  let imageInput = document.getElementById("img_pro");
//  let cvInput = document.getElementById("cv_pro");
//  let linkedinUrl = document.getElementById("linkedin_pro").value;
//  let user_name =document.getElementById("user_name").value;
///*بشيك على الحقول انو تم ادخالها*/
//  if (!firstName || !lastName || !birthDate || !gender || !phoneNumber || !user_name) {
//    showMessage("Please fill in all fields.",'info');
//    return;
//  }

//  if (imageInput.files.length === 0 && cvInput.files.length === 0) {/*اذا بدي اخلي الصورة اختياري*/ 
//    showMessage("Please upload your image and CV." ,'info');
//    return;
//  }

//  let imageFile = imageInput.files[0];
//  let cvFile = cvInput.files[0];

//  let readerImage = new FileReader();
//  let readerCV = new FileReader();

//  readerImage.onload = function (e) {
//    let imageBase64 = e.target.result;/*منخزن ناتج قراءة الصورة */

//    readerCV.onload = function (e) {
//      let cvBase64 = e.target.result;
///*فيه منخزنobject هون عملنا  */
//      let profileData = {
//        firstName,
//        lastName,
//        birthDate,
//        gender,
//        phoneNumber,
//        user_name,
//        image: imageBase64,
//        cv: cvBase64
//      };

//      if (linkedinUrl) {
//        profileData.linkedinUrl = linkedinUrl;/*اذا كان في رابط لا اللينكد ان خزنه في الاوبجيكت */
//      }

//      let currentEmail = localStorage.getItem("currentUser"); /*منجيب الايميل يلي مخزن في اللوكل */
//      if (currentEmail) {/*ي emailاذا في */
//        localStorage.setItem(`profile_${currentEmail}`, JSON.stringify(profileData)); /*بخزن البيانات في اللوك و بكون الايميل المفتاح تبعها*/
//        console.log("Data saved to localStorage!");
//        window.location.href = "profilePage.html";/*روح على البروفايل بيج */
//      } else {
//        console.log("User not logged in.");
//      }
//    };

//    readerCV.readAsDataURL(cvFile);
//  };
///* هون بتم قراءة cv img و برجعهم 383 */
//  readerImage.readAsDataURL(imageFile);
//}

////  تحميل البيانات في حال كنا بنعدل على البروفايل
//document.addEventListener("DOMContentLoaded", function () {/*هوني بستنى حتى تحمل محتويات الصفحة كاملة بعدين بنفذ الفنكشن */
//  const urlParams = new URLSearchParams(window.location.search);
//  const isEdit = urlParams.get("edit");

//  if (isEdit === "true") {
//    let currentEmail = localStorage.getItem("currentUser");
//    if (!currentEmail) return;

//    let profileData = JSON.parse(localStorage.getItem(`profile_${currentEmail}`));
//    if (!profileData) return;

//    // تعبئة الحقول القديمة
//    document.getElementById("fname_pro").value = profileData.firstName || "";
//    document.getElementById("lname_pro").value = profileData.lastName || "";
//    document.getElementById("date_pro").value = profileData.birthDate || "";
//    document.getElementById("select_pro").value = profileData.gender || "";
//    document.getElementById("ph_pro").value = profileData.phoneNumber || "";
//    document.getElementById("linkedin_pro").value = profileData.linkedinUrl || "";
//    document.getElementById("user_name").value = profileData.linkedinUrl || "";  
//  }
//});

//// ------------------------------
///*1. تحميل البيانات وتعبئة الفورم (عند تعديل مشروع)*/
//document.addEventListener("DOMContentLoaded", function () {
//  const form = document.querySelector("form");
//  if (!form) return;

//  const params = new URLSearchParams(window.location.search);
//  const projectId = params.get("id");
//  let allProjects = JSON.parse(localStorage.getItem("allPostedProjects")) || [];
//  let currentProject = null;

//  // تعبئة بيانات المشروع في حال التعديل
//  if (projectId) {
//    currentProject = allProjects.find(p => p.id == projectId);
//    if (!currentProject) {
//      showMessage("Project not found." , 'error');
//      window.location.href = "job-list.html";
//      return;
//    }

//    // تعبئة الفورم
//    ["title", "category", "description", "duration", "budget", "currency", "PaymentType", "email"].forEach(id => {
//      document.getElementById(id).value = currentProject[id.toLowerCase()] || "";
//    });
//  }

//  // إرسال النموذج
//  form.addEventListener("submit", async function (e) {
//    e.preventDefault();

//    const fields = ["title", "category", "description", "duration", "budget", "currency", "PaymentType", "email"];
//    const data = {};
//    fields.forEach(id => data[id.toLowerCase()] = document.getElementById(id).value);

//    const files = document.getElementById("files")?.files || [];
//    const currentEmail = localStorage.getItem("currentUser");

//    if (!currentEmail || data.email !== currentEmail) {
//      showMessage("You are not logged in or email doesn't match!" ,'error');
//      return;
//    }

//    // قراءة الملفات
//    const attachments = [];
//    for (let file of files) {
//      attachments.push(await readFileAsBase64(file));
//    }

//    const projectData = {
//      id: projectId ? currentProject.id : Date.now(),
//      ...data,
//      attachments: projectId ? currentProject.attachments : attachments
//    };

//    if (projectId) {
//      // تعديل
//      allProjects = allProjects.map(p => p.id == projectId ? projectData : p);
//      showMessage("Project updated successfully!",'success');
//    } else {
//      // إضافة
//      allProjects.push(projectData);
//      showMessage("Project submitted successfully!",'sccess');
//    }

//    localStorage.setItem("allPostedProjects", JSON.stringify(allProjects));
//    window.location.href = "job-list.html";
//  });
//});

//// وظيفة قراءة ملف كـ Base64
//function readFileAsBase64(file) {
//  return new Promise((resolve) => {
//    const reader = new FileReader();
//    reader.onload = (e) => {
//      resolve({
//        name: file.name,
//        data: e.target.result
//      });
//    };
//    reader.readAsDataURL(file);
//  });
//}
///* 2. عرض قائمة المشاريع*/
//document.addEventListener("DOMContentLoaded", () => {
//  const container = document.getElementById("job-listings");
//  if (!container) return;

//  const allProjects = JSON.parse(localStorage.getItem("allPostedProjects")) || [];
//  if (allProjects.length === 0) {
//    container.innerHTML = `<p class="text-center mt-5 textm fs-3">No jobs yet.</p>`;
//    return;
//  }

//  const currentUser = localStorage.getItem("currentUser");

//  allProjects.forEach(project => {
//    const isOwner = currentUser && project.email === currentUser;
//    const profileKey = `profile_${project.email}`;
//    const profile = JSON.parse(localStorage.getItem(profileKey));
//    const image = profile?.image || "assets/img/default.png";
//    const fullName = profile ? `${profile.firstName} ${profile.lastName}` : "Unknown User";

//    // أزرار التحكم
  

//    const controls = `
//    <div class="d-flex mb-3 align-items-center justify-content-end flex-wrap gap-2">
//      ${!isOwner ? `
//        <a class="btn btn-primary btn-sm" href="project-details.html?id=${project.id}">Apply Now</a>
//      ` : `
//        <button class="btn btn-warning btn-sm edit-project" data-id="${project.id}">Edit</button>
//        <button class="btn btn-danger btn-sm delete-project" data-id="${project.id}">Delete</button>
//      `}
//    </div>
//  `;
  


//    container.innerHTML += `
//      <div class="job-item p-4 mb-4">
//        <div class="row g-4">
//          <div class="col-md-8 d-flex align-items-center">
//            <img class="flex-shrink-0 img-fluid border rounded" src="${image}" style="width: 100px; height: 100px;" />
//            <div class="text-start ps-4">
//              <h5 class="mb-1 textmi fs-4">${project.title}</h5>
//              <p class="mb-1 text-muted textm fs-5"> Posted by: ${fullName}</p>
//              <span class="me-3 text-muted textm fs-5"><i class="fas fa-briefcase text-primary me-2"></i>${project.category}</span>
//              <span class="me-3 text-muted textm fs-5 "><i class="far fa-clock text-primary me-2"></i>${project.paymentType}</span>
//              <span><i class="far fa-money-bill-alt text-primary me-2 "></i>${project.budget} ${project.currency}</span>
//            </div>
//          </div>
//          <div class="col-md-4 d-flex flex-column align-items-end justify-content-center">
//            ${controls}
//            <small><i class="far fa-calendar-alt text-primary me-2"></i>Duration: ${project.duration} Days</small>
//          </div>
//        </div>
//      </div>
//    `;
//  });
//});

//// حذف وتعديل المشاريع
//document.addEventListener("click", e => {
//  const id = e.target.dataset.id;

//  if (e.target.classList.contains("delete-project")) {
//    if (confirm("Are you sure you want to delete this project?")) {
//      let allProjects = JSON.parse(localStorage.getItem("allPostedProjects")) || [];
//      allProjects = allProjects.filter(p => p.id != id);
//      localStorage.setItem("allPostedProjects", JSON.stringify(allProjects));
//      location.reload();
//    }
//  }

//  if (e.target.classList.contains("edit-project")) {
//    window.location.href = `post_job.html?id=${id}`;
//  }
//});
//function cleanProductsStorage() {
//  const rawProducts = JSON.parse(localStorage.getItem("products")) || [];
//  const cleaned = rawProducts.filter(
//    (p) => p && typeof p === "object" && p.name && p.description && p.price != null
//  );
//  localStorage.setItem("products", JSON.stringify(cleaned));
//}

///*Applicants*/
//// Define the rejectApplicant function globally
//function rejectApplicant(projectId, applicantEmail) {
//  const allProjects = JSON.parse(localStorage.getItem("allPostedProjects")) || [];

//  // Find the project by ID and remove the applicant from the applied users list
//  const updatedProjects = allProjects.map(project => {
//    if (project.id === Number(projectId)) {
//      project.appliedUsers = (project.appliedUsers || []).filter(
//        email => email.trim() !== applicantEmail.trim()
//      );
//    }
//    return project;
//  });

//  // Save the updated list of projects back to localStorage
//  localStorage.setItem("allPostedProjects", JSON.stringify(updatedProjects));
//  location.reload(); // Reload the page to update the view
//}

//document.addEventListener("DOMContentLoaded", () => {
//  const container = document.getElementById("applicants-container");
//  const currentUser = localStorage.getItem("currentUser");

//  if (!currentUser) {
//    container.innerHTML = `<div class="alert alert-danger">You must be logged in to view this page.</div>`;
//    return;
//  }

//  const allProjects = JSON.parse(localStorage.getItem("allPostedProjects")) || [];
//  const myProjects = allProjects.filter(p => p.email === currentUser);

//  if (myProjects.length === 0) {
//    container.innerHTML = `<p class="text-muted">You haven’t posted any projects yet.</p>`;
//    return;
//  }

//  myProjects.forEach(project => {
//    const applicants = project.appliedUsers || [];

//    let html = `
//      <div class="card mb-4">
//        <div class="card-body">
//          <h5 class="card-title">${project.title}</h5>
//          <h6 class="card-subtitle mb-2 text-muted">${project.category} | ${project.budget} ${project.currency}</h6>
//          <p class="card-text">Total Applicants: <strong>${applicants.length}</strong></p>
//    `;

//    if (applicants.length === 0) {
//      html += `<p class="text-muted">No applicants yet.</p>`;
//    } else {
//      html += `<ul class="list-group list-group-flush mt-3">`;

//      applicants.forEach((email, index) => {
//        const profile = JSON.parse(localStorage.getItem(`profile_${email}`));
//        const name = profile ? `${profile.firstName} ${profile.lastName}` : email;

//        html += `
//        <div class="card mb-3">
//          <div class="card-header bg-success text-white">Request</div>
//          <div class="card-body d-flex justify-content-between align-items-start flex-wrap">
//            <div>
//              <strong>${name}</strong> <span class="text-muted">(${email})</span><br>
//              <a href="profilePage.html?email=${email}" class="btn btn-sm btn-outline-success mt-2">show profile</a>
//            </div>
//            <div class="mt-2 mt-md-0">
//              <a href="mailto:${email}?subject=Your application for the project has been accepted&body=You have been accepted into the project '${project.title}'." class="btn btn-sm btn-success me-2">acceptance</a>
//              <button class="btn btn-sm btn-danger" onclick="rejectApplicant(${project.id}, '${email}')">reject</button>
//            </div>
//          </div>
//        </div>
//        `;
//      });

//      html += `</ul>`;
//    }

//    html += `</div></div>`;
//    container.innerHTML += html;
//  });
//});


////////////////////////stores///////////////////////////////////////////


//function getLoggedInUserEmail() {
//  const user = localStorage.getItem("currentUser");
//  try {
//    const parsed = JSON.parse(user);
//    return parsed.email;
//  } catch (e) {
//    // إذا القيمة هي الإيميل مباشرة
//    return user;
//  }
//}


//function saveProducts(products) {
//  localStorage.setItem("products", JSON.stringify(products));
//}

//function getProducts() {
//  return JSON.parse(localStorage.getItem("products")) || [];
//}

//// ================== add_product.html ==================

//// حفظ المنتج مع الكمية الأصلية
//if (window.location.pathname.includes("add_product.html")) {
//  const form = document.getElementById("addProductForm");
//  const nameInput = document.getElementById("name");
//  const descriptionInput = document.getElementById("description");
//  const priceInput = document.getElementById("price");
//  const imageInput = document.getElementById("image");
//  const quantityInput = document.getElementById("Quantity");

//  const editIndex = localStorage.getItem("editProductIndex");
//  if (editIndex !== null) {
//    const products = getProducts();
//    const product = products[editIndex];
//    if (product) {
//      nameInput.value = product.name;
//      descriptionInput.value = product.description;
//      priceInput.value = product.price;
//      quantityInput.value = product.quantity || 1;
//      localStorage.setItem("oldImage", product.image);
//    }
//  }

//  form.addEventListener("submit", (e) => {
//    e.preventDefault();
//    const reader = new FileReader();
//    const file = imageInput.files[0];
//    reader.onload = function () {
//      const imageURL = file ? reader.result : localStorage.getItem("oldImage");
//      const product = {
//        name: nameInput.value,
//        description: descriptionInput.value,
//        price: parseFloat(priceInput.value),
//        quantity: parseInt(quantityInput.value),
//        image: imageURL,
//        owner: getLoggedInUserEmail(),
//      };
//      const products = getProducts();
//      if (editIndex !== null) {
//        products[editIndex] = product;
//        localStorage.removeItem("editProductIndex");
//        localStorage.removeItem("oldImage");
//      } else {
//        products.push(product);
//      }
//      saveProducts(products);
//      window.location.href = "stores.html";
//    };
//    if (file) reader.readAsDataURL(file);
//    else reader.onload();
//  });
//}


//// ================== store.html ==================

//if (window.location.pathname.includes("stores.html")) {
//  const container = document.getElementById("productsContainer");
//  const currentUserEmail = getLoggedInUserEmail();
//  const products = getProducts();

//  function renderStore() {
//    if (products.length === 0) {
//      container.innerHTML = `<p class="text-muted text-center">No products available.</p>`;
//      return;
//    }
//    container.innerHTML = "";
//    products.forEach((product, index) => {
//      if (!product) return; // تجاهل العناصر null أو undefined
    
//      const isOwner = product.owner === currentUserEmail;
//      // باقي الكود...
    
//      const card = document.createElement("div");
//      card.className = "col-md-4 mb-4";
//      card.innerHTML = `
//  <div class="card border-0 shadow-sm rounded-3 overflow-hidden mt-3" style="transition: transform 0.3s; cursor: pointer; 
//}
//">
//    <img src="${product.image}" class="card-img-top" alt="${product.name}" style="height: 250px; object-fit: cover;">
//    <div class="card-body d-flex flex-column justify-content-between ">
//      <h5 class="card-title text-truncate mb-2 fs-3" title="${product.name}">
//        ${product.name}
//      </h5>
//      <p class="card-text text-muted small  mb-2 fs-3" style="min-height: 50px;">
//        ${product.description}
//      </p>
//      <div class="d-flex justify-content-between align-items-center mb-3">
//        <span class="text-success fw-bold fs-2 ms-2">${product.price} JD</span>
//        <span class="badge bg-primary fs-4">Qty: ${product.quantity}</span>
//      </div>
//      <div class="d-grid gap-2">
//        ${
//          isOwner
//            ? `
//              <button class="btn btn-outline-warning btn-sm edit-btn" data-index="${index}">
//                <i class="bi bi-pencil-square"></i> Edit
//              </button>
//              <button class="btn btn-outline-danger btn-sm delete-btn" data-index="${index}">
//                <i class="bi bi-trash"></i> Delete
//              </button>
//            `
//            : `
//              <button class="btn btn-primary btn-lg  add-to-cart-btn" data-index="${index}">
//                <i class="bi bi-cart-plus"></i> Add to Cart
//              </button>
//            `
//        }
//      </div>
//    </div>
//  </div>
//`;

//      container.appendChild(card);
//    });

//    document.querySelectorAll(".edit-btn").forEach(btn => {
//      btn.addEventListener("click", (e) => {
//        const index = e.target.dataset.index;
//        localStorage.setItem("editProductIndex", index);
//        window.location.href = "add_product.html";
//      });
//    });

//    document.querySelectorAll(".delete-btn").forEach(btn => {
//      btn.addEventListener("click", (e) => {
//        const index = e.target.dataset.index;
//        products.splice(index, 1);
//        saveProducts(products);
//        renderStore();
//      });
//    });

//    document.querySelectorAll(".add-to-cart-btn").forEach(btn => {
//      btn.addEventListener("click", (e) => {
//        const index = e.target.dataset.index;
//        const cart = JSON.parse(localStorage.getItem("cart")) || [];
//        const productToAdd = {...products[index], cartQuantity: 1};
//        cart.push(productToAdd);
//        localStorage.setItem("cart", JSON.stringify(cart));
//      });
//    });
//  }
//  cleanProductsStorage();
//  renderStore();
//}










//// ================== cart.html ==================/


//if (window.location.pathname.includes("cart.html")) {
//  const cartContainer = document.getElementById("cartContainer");
//  let cart = JSON.parse(localStorage.getItem("cart")) || [];

//  function renderCart() {
//    cartContainer.innerHTML = "";

//    if (cart.length === 0) {
//      cartContainer.innerHTML = "<p class='text-center text-success'>Cart is empty</p>";
//      return;
//    }

//    cart.forEach((product, index) => {
//      const totalPrice = (product.price * product.cartQuantity).toFixed(2);
//      const maxQuantity = product.availableQuantity; // نفترض عندك هذا الحقل
      
//      const card = document.createElement("div");
//      card.className = "col-md-4 mb-4";
      
//      card.innerHTML = `
//      <div class="card border-0 shadow-sm rounded-3 h-100">
//        <img src="${product.image}" class="card-img-top" style="height: 220px; object-fit: cover; border-top-left-radius: .75rem; border-top-right-radius: .75rem;">
//        <div class="card-body d-flex flex-column">
//          <h5 class="card-title text-truncate fs-3">${product.name}</h5>
//          <p class="card-text small text-muted fs-3">${product.description}</p>
//          <p class="card-text fw-bold text-success mb-2 fs-4">${totalPrice} JD</p>
          
//          <div class="d-flex justify-content-center align-items-center mb-2">
//            <button class="btn btn-outline-primary btn-md py-0 px-2" 
//              onclick="changeQuantity(${index}, -1)"
//              ${product.cartQuantity <= 1 ? 'disabled' : ''}>
//              <i class="bi bi-dash"></i>
//            </button>
//            <span class="fw-bold px-2">${product.cartQuantity}</span>
//            <button class="btn btn-outline-primary btn-md py-0 px-2"
//              onclick="changeQuantity(${index}, 1)"
//              ${product.cartQuantity >= product.quantity ? 'disabled' : ''}>
//              <i class="bi bi-plus"></i>
//            </button>
//          </div>
          
//          <div class="mt-auto">
//            <button class="btn btn-danger btn-md w-100 mb-2" onclick="removeFromCart(${index})">
//              <i class="bi bi-trash"></i> Delete
//            </button>
//            <button class="btn btn-success btn-md w-100" onclick="buySingleProduct(${index})">
//              <i class="bi bi-cart-check"></i> Buy
//            </button>
//          </div>
//        </div>
//      </div>
//    `;
    
//cartContainer.appendChild(card);
//    })}
//  ;
    

//  window.changeQuantity = function(index, change) {
//    if (cart[index].cartQuantity + change > 0) {
//      cart[index].cartQuantity += change;
//      localStorage.setItem("cart", JSON.stringify(cart));
//      renderCart();
//    }
//  }

//  window.removeFromCart = function(index) {
//    cart.splice(index, 1);
//    localStorage.setItem("cart", JSON.stringify(cart));
//    renderCart();
//  }

//  renderCart();
//}

//function buySingleProduct(index) {
//  const cart = JSON.parse(localStorage.getItem("cart")) || [];
//  const selectedProduct = cart[index];
//  localStorage.setItem("singleProductToBuy", JSON.stringify(selectedProduct));
//  window.location.href = "for_ordder.html";
//}

//// ================== for_ordder.html ==================

//if (window.location.pathname.includes("for_ordder.html")) {
//  const form = document.getElementById("deliveryForm");

//  if (form) {
//    form.addEventListener("submit", function (e) {
//      e.preventDefault();

//      const fullName = document.getElementById("fullName").value;
//      const phone = document.getElementById("phoneNumber").value;
//      const address = document.getElementById("fulladdress").value;

//      const selectedProduct = JSON.parse(localStorage.getItem("singleProductToBuy"));
//      const products = getProducts();

//      const originalProduct = products.find(p =>
//        p.name === selectedProduct.name && p.description === selectedProduct.description
//      );

//      const productToOrder = {
//        ...selectedProduct,
//        owner: originalProduct?.owner || "unknown"
//      };

//      const email = localStorage.getItem("currentUser"); // 🔥 الإيميل من localStorage مش من الفورم

//      const orderInfo = {
//        fullName,
//        email,
//        phone,
//        address,
//        date: new Date().toLocaleString(),
//        cartItems: [productToOrder],
//      };

//      const allOrders = JSON.parse(localStorage.getItem("allOrders")) || [];
//      allOrders.push(orderInfo);
//      localStorage.setItem("allOrders", JSON.stringify(allOrders));

//      let cart = JSON.parse(localStorage.getItem("cart")) || [];
//      cart = cart.filter(item =>
//        !(item.name === selectedProduct.name && item.description === selectedProduct.description)
//      );
//      localStorage.setItem("cart", JSON.stringify(cart));

//      localStorage.removeItem("singleProductToBuy");
//      window.location.href = "stores.html";
//    });
//  }
//}



//////////////////////// عرض الطلبات في my_product.html //////////////////////

//if (window.location.pathname.includes("my_product.html")) {
//  const containerpro = document.getElementById("my_product");
//  const allOrders = JSON.parse(localStorage.getItem("allOrders")) || [];

//  const currentUserEmail = getLoggedInUserEmail();
//  containerpro.innerHTML = "";

//  if (!currentUserEmail) {
//    containerpro.innerHTML = "<div class='alert alert-danger text-center'>⚠️ You must be logged in to view orders.</div>";
//  } else {
//    const userOrders = allOrders.filter(order =>
//      order.cartItems.some(item => item.owner === currentUserEmail)
//    );

//    if (userOrders.length === 0) {
//      containerpro.innerHTML = "<div class='alert alert-info text-center'>🚫 No orders for your products yet.</div>";
//    } else {
//      userOrders.forEach((order, index) => {
//        const ownedItems = order.cartItems.filter(item => item.owner === currentUserEmail);

//        const cartHTML = ownedItems.length > 0
//        ? ownedItems.map(item => {
//            const quantity = item.cartQuantity || 1;
//            const unitPrice = parseFloat(item.price) || 0;
//            const totalPrice = (quantity * unitPrice).toFixed(2);
      
//            return `
//            <li class="list-group-item d-flex justify-content-between align-items-center">
//              <div>
//                <strong>${item.name}</strong> <br>
//                <small class="text-muted">Quantity: ${quantity}</small>
//              </div>
//              <span class="badge bg-primary rounded-pill">${totalPrice} JD</span>
//            </li>
//            `;
//          }).join("")
//        : "<li class='list-group-item text-muted'>No items</li>";
      


//        const cardId = `order-${index}`;
//        const orderStatusClass = order.status === 'Delivered' ? 'bg-success' : 'bg-warning';
//        const orderStatusText = order.status ===  'Delivered' ? 'Delivered' : 'Under Delivery';

//        containerpro.innerHTML += `
//          <div class="card mb-4 shadow-lg border border-success bg-white text-dark rounded-4" id="${cardId}">
//            <div class="card-header d-flex justify-content-between align-items-center ${orderStatusClass} bg-opacity-25 rounded-4 rounded-bottom-0">
//              <span>🧾 <strong>Order No:</strong> ${index + 1}</span>
//              <span class="badge ${orderStatusClass} text-dark px-3 py-2">${orderStatusText}</span>
//            </div>
//            <div class="card-body">
//              <p class="mb-2"><i class="bi bi-person-fill"></i> <strong>Name:</strong> ${order.fullName}</p>
//              <p class="mb-2"><i class="bi bi-envelope-fill"></i> <strong>Email:</strong> ${order.email}</p>
//              <p class="mb-2"><i class="bi bi-telephone-fill"></i> <strong>Phone:</strong> ${order.phone}</p>
//              <p class="mb-2"><i class="bi bi-geo-alt-fill"></i> <strong>Address:</strong> ${order.address}</p>
//              <p class="mb-3"><i class="bi bi-calendar-event-fill"></i> <strong>Date:</strong> ${order.date}</p>

//              <h6 class="mt-3 mb-2 text-success"><i class="bi bi-cart-fill"></i> Ordered Items:</h6>
//              <ul class="list-group list-group-flush">
//                ${cartHTML}
                
//              </ul>

//              <div class="mt-4 d-flex gap-2">
//                ${order.status !== 'Delivered' ? 
//                  `<button class="btn btn-success" onclick="markAsDelivered('${cardId}', '${order.date}')">Mark as Delivered</button>` 
//                  : ''}
//                <button class="btn btn-danger" onclick="deleteOrder('${cardId}', '${order.date}')">Delete Order</button>
//              </div>
//            </div>  
//          </div>
//        `;
//      });
//    }
//  }
//}



//function markAsDelivered(cardId, orderDate) {
//  const allOrders = JSON.parse(localStorage.getItem("allOrders")) || [];
//  const orderIndex = allOrders.findIndex(order => order.date === orderDate);

//  if (orderIndex === -1) return;

//  const order = allOrders[orderIndex];
//  order.status = 'Delivered';
//  localStorage.setItem("allOrders", JSON.stringify(allOrders));

//  // حساب الأرباح الكلية
//  const currentRevenue = parseFloat(localStorage.getItem("totalRevenue") || "0");
//  const orderTotal = order.cartItems.reduce((total, item) => {
//    const price = parseFloat(item.price) || 0;
//    const quantity = item.cartQuantity || 1;
//    return total + (price * quantity);
//  }, 0);
//  const newRevenue = (currentRevenue + orderTotal).toFixed(2);
//  localStorage.setItem("totalRevenue", newRevenue);

//  // حساب أرباح المستخدم الشهري
//  const currentUserEmail = getLoggedInUserEmail();
//  const monthlyRevenuePerUser = JSON.parse(localStorage.getItem("monthlyRevenuePerUser")) || {};
//  const now = new Date();
//  const key = `${now.getFullYear()}-${now.getMonth() + 1}`; // مثلا 2025-4

//  if (!monthlyRevenuePerUser[currentUserEmail]) {
//    monthlyRevenuePerUser[currentUserEmail] = {};
//  }

//  monthlyRevenuePerUser[currentUserEmail][key] = (monthlyRevenuePerUser[currentUserEmail][key] || 0) + orderTotal;
//  localStorage.setItem("monthlyRevenuePerUser", JSON.stringify(monthlyRevenuePerUser));

//  // ⭐ نقص الكمية من المنتجات + حذف المنتج إذا صارت كميته صفر
//  let products = JSON.parse(localStorage.getItem("products")) || [];
//  order.cartItems.forEach(orderedItem => {
//    const productIndex = products.findIndex(p => p.name === orderedItem.name && p.owner === orderedItem.owner);
//    if (productIndex !== -1) {
//      products[productIndex].quantity -= orderedItem.cartQuantity;
//      if (products[productIndex].quantity <= 0) {
//        products.splice(productIndex, 1); // حذف المنتج نهائيًا
//      }
//    }
//  });
//  localStorage.setItem("products", JSON.stringify(products));

//  // تحديث الواجهة
//  const card = document.getElementById(cardId);
//  if (card) {
//    const statusBadge = card.querySelector('.badge');
//    if (statusBadge) {
//      statusBadge.textContent = "Delivered";
//      statusBadge.classList.remove('bg-warning');
//      statusBadge.classList.add('bg-success');
//    }
//    const deliverButton = card.querySelector('.btn-success');
//    if (deliverButton) {
//      deliverButton.style.display = 'none';
//    }
//  }

//  showMessage("Order marked as delivered." ,'success');

//  const revenueDisplay = document.getElementById("revenueDisplay");
//  if (revenueDisplay) {
//    revenueDisplay.textContent = `Total Revenue: ${newRevenue} JD`;
//  }

//  // ⭐ تحديث المبيعات الشهرية
//  const monthlyRevenue = JSON.parse(localStorage.getItem("monthlyRevenue")) || {};
//  const orderMonth = new Date(orderDate).getMonth() + 1; // getMonth ترجع من 0 إلى 11
//  const orderYear = new Date(orderDate).getFullYear();
//  const keyForMonthly = `${orderYear}-${orderMonth}`; // مثال: "2025-4"

//  monthlyRevenue[keyForMonthly] = (monthlyRevenue[keyForMonthly] || 0) + orderTotal;
//  localStorage.setItem("monthlyRevenue", JSON.stringify(monthlyRevenue));
//  // ⭐ تخزين عدد الطلبات المنجزة شهريًا
//const deliveredOrdersPerMonth = JSON.parse(localStorage.getItem("deliveredOrdersPerMonth")) || {};
//const deliveredKey = `${orderYear}-${orderMonth}`;

//deliveredOrdersPerMonth[deliveredKey] = (deliveredOrdersPerMonth[deliveredKey] || 0) + 1;

//localStorage.setItem("deliveredOrdersPerMonth", JSON.stringify(deliveredOrdersPerMonth));

//}

//function deleteOrder(cardId, index) {
//  const allOrders = JSON.parse(localStorage.getItem("allOrders")) || [];
//  allOrders.splice(index, 1);
//  localStorage.setItem("allOrders", JSON.stringify(allOrders));

//  const card = document.getElementById(cardId);
//  if (card) card.remove();
//}
///////////////////////////////////////////////////////////////
// // تحميل معلومات المستخدم من localStorage
// /*const currentEmail = localStorage.getItem("currentUser");
// const profile = JSON.parse(localStorage.getItem(`profile_${currentEmail}`));

// if (profile) {
//   document.getElementById("userName").textContent = `${profile.firstName} ${profile.lastName}`;
//   document.getElementById("userEmail").textContent = currentEmail;
//   document.getElementById("profileImage").src = profile.image || "default-avatar.png";
// }
//// الإحصائيات
//// عدد المشاريع اللي نشرها المستخدم
//document.getElementById("publishedProjects").textContent =
// (JSON.parse(localStorage.getItem("allPostedProjects")) || []).filter(p => p.email === currentEmail).length;

//// عدد المنتجات اللي أضافها المستخدم
//document.getElementById("myProducts").textContent =
// (JSON.parse(localStorage.getItem("products")) || []).filter(p => p.owner === currentEmail).length;
//// عدد المستخدمين اللي قدموا على مشاريعي
//document.getElementById("appliedProjects").textContent =
// (JSON.parse(localStorage.getItem("allPostedProjects")) || [])
//   .filter(p => p.email === currentEmail)
//   .reduce((acc, curr) => acc + (curr.appliedUsers?.length || 0), 0);
//// عدد الطلبات التي تخص المستخدم
//const allOrders = JSON.parse(localStorage.getItem("allOrders")) || [];
//const userOrdersCount = allOrders.filter(order =>
//  order.cartItems.some(item => item.owner === currentEmail)
//).length;
//document.getElementById("userOrdersCount").textContent = userOrdersCount;
//// رسم المبيعات الشهرية (Area Chart)
//document.addEventListener("DOMContentLoaded", function () {
//  const currentUserEmail = getLoggedInUserEmail();
//  const monthlyRevenuePerUser = JSON.parse(localStorage.getItem("monthlyRevenuePerUser")) || {};

//  const months = [
//    "January", "February", "March", "April", "May", "June",
//    "July", "August", "September", "October", "November", "December"
//  ];
//  const monthNumbers = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"];
//  const currentYear = new Date().getFullYear();

//  const userRevenue = monthlyRevenuePerUser[currentUserEmail] || {};

//  const revenues = monthNumbers.map(month => {
//    const key = `${currentYear}-${month}`;
//    return userRevenue[key] || 0;
//  });

//  const ctx = document.getElementById('monthlySalesChart').getContext('2d');
//  new Chart(ctx, {
//    type: 'line', // Area chart via Line chart with fill
//    data: {
//      labels: months,
//      datasets: [{
//        label: `Sales in ${currentYear} (JD)`,
//        data: revenues,
//        fill: true,
//        backgroundColor: 'rgba(25, 135, 84, 0.2)', // Light green
//        borderColor: 'rgba(25, 135, 84, 1)', // Bootstrap green
//        borderWidth: 2,
//        tension: 0.4 // Smooth curve
//      }]
//    },
//    options: {
//      responsive: true,
//      scales: {
//        y: {
//          beginAtZero: true,
//          title: {
//            display: true,
//            text: "Sales (JD)"
//          }
//        },
//        x: {
//          title: {
//            display: true,
//            text: "Months"
            
//          }
//        }
//      },
//      plugins: {
//        legend: {
//          display: true,
//          labels: {
//            color: '#198754' // Text color green
//          }
//        }
//      }
//    }
//  });
//});

//document.addEventListener("DOMContentLoaded", function () {
//  const deliveredOrdersPerMonth = JSON.parse(localStorage.getItem("deliveredOrdersPerMonth")) || {};

//  const months = [
//    "January", "February", "March", "April", "May", "June",
//    "July", "August", "September", "October", "November", "December"
//  ];
//  const monthNumbers = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"];
//  const currentYear = new Date().getFullYear();

//  const ordersCount = monthNumbers.map(month => {
//    const key = `${currentYear}-${month}`;
//    return deliveredOrdersPerMonth[key] || 0;
//  });

//  const ctx = document.getElementById('deliveredOrdersChart').getContext('2d');
//  new Chart(ctx, {
//    type: 'bar', // رسم بياني عمودي
//    data: {
//      labels: months,
//      datasets: [{
//        label: `Delivered Orders in ${currentYear}`,
//        data: ordersCount,
//        backgroundColor: 'rgba(25, 135, 84, 0.2)', // Light green
//        borderColor: 'rgba(25, 135, 84, 1)', // Bootstrap green
//        borderWidth: 1
//      }]
//    },
//    options: {
//      responsive: true,
//      scales: {
//        y: {
//          beginAtZero: true,
//          title: {
//            display: true,
//            text: "Number of Delivered Orders",
//            color: "rgba(25, 135, 84, 0.82)",
//            font: { weight: 'bold' }
//          },
//          ticks: { color: "#198754'" }
//        },
//        x: {
//          title: {
//            display: true,
//            text: "Months",
//            color: "#198754'",
//            font: { weight: 'bold' }
//          },
//          ticks: { color: "#198754" }
//        }
//      },
//      plugins: {
//        legend: {
//          labels: {
//            color: "#198754'"
//          }
//        }
//      }
//    }
//  });
//});







//  // تصنيف الطلبات إلى مكتملة وغير مكتملة
//  const completedOrders = allOrders.filter(order => order.status === "Delivered");
//  const incompleteOrders = allOrders.filter(order => order.status !== "Delivered");

//  // حساب النسب
//  const completedPercentage = (completedOrders.length / allOrders.length) * 100;
//  const incompletePercentage = 100 - completedPercentage;

//  // رسم الدونت شارت
//  const ctx = document.getElementById('orderCompletionChart').getContext('2d');
//  const orderCompletionChart = new Chart(ctx, {
//    type: 'doughnut',
//    data: {
//      labels: ['complete', 'un complete'], // الفئات
//      datasets: [{
//        label: 'Order status',
//        data: [completedPercentage, incompletePercentage], // النسب المحسوبة
//        backgroundColor: ['#198754', '#4caf50'], // الألوان
//        borderColor: '#ffffff',
//        borderWidth: 2
//      }]
//    },
//    options: {
//      responsive: true,
//      maintainAspectRatio: false,
//      cutout: '50%',
//      plugins: {
//        legend: {
//          position: 'top',
//        },
//        tooltip: {
//          callbacks: {
//            label: function(tooltipItem) {
//              return tooltipItem.label + ': ' + tooltipItem.raw.toFixed(2) + '%';
//            }
//          }
//        }
//      }
//    }
    
//  });*/
//  ///////////////////////////////////////////////////////posted in home page from admin /////////////

//    document.addEventListener('DOMContentLoaded', function () {
//          const posts = JSON.parse(localStorage.getItem('allPosts')) || [];
//    const container = document.getElementById('postsContainer');

//    const currentUserEmail = localStorage.getItem('currentUser');
//    const isAdmin = currentUserEmail === "admin@hotmail.com";

//    if (posts.length === 0) {
//        container.innerHTML = '<p class="text-center text-muted">لا يوجد منشورات حتى الآن.</p>';
//    return;
//          }
      
//          posts.reverse().forEach((post, index) => {
//            const postCol = document.createElement('div');
//    postCol.className = 'col-12 mb-4';

//    const card = document.createElement('div');
//    card.className = 'card text-dark';

//    if (post.imageUrl) {
//        card.innerHTML = `
//                <div class="d-flex flex-row-reverse">
//                  <img src="${post.imageUrl}" class="card-img-right rounded-start" alt="img post153200" style="width: 500px; height: 350px; object-fit: cover; flex-shrink: 0;">
//                  <div class="card-body" style="flex-grow: 1;">
//                    <h5 class="card-title fs-3 text-dark">${post.title}</h5>
//                    <p class="card-text fs-5">${post.content}</p>
//                  </div>
//                </div>
//                <div class="card-footer text-muted small text-end">${new Date(post.date).toLocaleString()}</div>
//              `;
//            } else {
//        card.innerHTML = `
//                <div class="card-body">
//                  <h5 class="card-title fs-3 text-dark">${post.title}</h5>
//                  <p class="card-text fs-5">${post.content}</p>
//                </div>
//                <div class="card-footer text-muted small text-end">${new Date(post.date).toLocaleString()}</div>
//              `;
//            }

//    if (isAdmin) {
//              const actionButtons = document.createElement('div');
//    actionButtons.className = 'card-footer text-center';
//    actionButtons.innerHTML = `
//    <button class="btn btn-warning me-2" data-bs-toggle="modal" data-bs-target="#editModal${index}">edit</button>
//    <button class="btn btn-danger" onclick="deletePost(${index})">delete</button>
//    `;
//    card.appendChild(actionButtons);
//            }

//    postCol.appendChild(card);
//    container.appendChild(postCol);

//    // إضافة المودال بعد الكارد
//    const modal = document.createElement('div');
//    modal.innerHTML = `
//    <div class="modal fade" id="editModal${index}" tabindex="-1" aria-labelledby="editModalLabel${index}" aria-hidden="true">
//        <div class="modal-dialog">
//            <div class="modal-content">
//                <div class="modal-header">
//                    <h5 class="modal-title" id="editModalLabel${index}">Edit post</h5>
//                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
//                </div>
//                <div class="modal-body">
//                    <form id="editPostForm${index}">
//                        <div class="mb-3">
//                            <label for="title${index}" class="form-label">Post title</label>
//                            <input type="text" class="form-control" id="title${index}" value="${post.title}">
//                        </div>
//                        <div class="mb-3">
//                            <label for="content${index}" class="form-label">Post content</label>
//                            <textarea class="form-control" id="content${index}" rows="3">${post.content}</textarea>
//                        </div>
//                    </form>
//                </div>
//                <div class="modal-footer">
//                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">close</button>
//                    <button type="button" class="btn btn-primary" onclick="savePostChanges(${index})">Save changes</button>
//                </div>
//            </div>
//        </div>
//    </div>
//    `

//    document.body.appendChild(modal);
//          });
//        });

//    // حذف بوست
//    function deletePost(index) {
//        let posts = JSON.parse(localStorage.getItem('allPosts')) || [];
//    if (confirm('Are you sure you want to delete this post?')) {
//        posts.splice(posts.length - 1 - index, 1);
//    localStorage.setItem('allPosts', JSON.stringify(posts));
//    location.reload();
//        }
//      }

//    // حفظ التعديلات
//    function savePostChanges(index) {
//        let posts = JSON.parse(localStorage.getItem('allPosts')) || [];
//    const titleInput = document.getElementById(`title${index}`);
//    const contentInput = document.getElementById(`content${index}`);

//    posts[posts.length - 1 - index].title = titleInput.value;
//    posts[posts.length - 1 - index].content = contentInput.value.trim();

//    localStorage.setItem('allPosts', JSON.stringify(posts));
//    location.reload();
//      }




//    document.addEventListener('DOMContentLoaded', function () {
//  const postsContainer = document.getElementById('postsContainer');
//    const scrollAmount = 1312; // المسافة التي سيتحرك بها السكروول
//    const containerWidth = postsContainer.scrollWidth; // عرض الحاوية
//    const visibleWidth = postsContainer.clientWidth; // العرض المرئي
//    const totalScrollWidth = containerWidth - visibleWidth; // المسافة الكلية للتمرير

//  let autoScrollInterval = setInterval(() => {
//        // التمرير التلقائي
//        postsContainer.scrollBy({
//            left: scrollAmount,
//            behavior: 'smooth'
//        });

//    // إذا وصلنا إلى آخر المنشور، نعيد التمرير إلى البداية
//    if (postsContainer.scrollLeft >= totalScrollWidth) {
//        postsContainer.scrollTo({
//            left: 0, // العودة للبداية
//            behavior: 'smooth' // تأثير التمرير السلس
//        });
//    }
//  }, 6000); // التمرير التلقائي كل 3 ثواني

//  // إيقاف التمرير التلقائي عند وضع الماوس
//  postsContainer.addEventListener('mouseover', () => {
//        clearInterval(autoScrollInterval); // إيقاف التمرير التلقائي عند وضع الماوس
//  });

//  // استئناف التمرير التلقائي عند إزالة الماوس
//  postsContainer.addEventListener('mouseout', () => {
//        autoScrollInterval = setInterval(() => {
//            postsContainer.scrollBy({
//                left: scrollAmount,
//                behavior: 'smooth'
//            });

//            // إذا وصلنا إلى آخر المنشور، نعيد التمرير إلى البداية
//            if (postsContainer.scrollLeft >= totalScrollWidth) {
//                postsContainer.scrollTo({
//                    left: 0, // العودة للبداية
//                    behavior: 'smooth' // تأثير التمرير السلس
//                });
//            }
//        }, 6000); // استئناف التمرير التلقائي
//  });

//    // إضافة التمرير اليدوي باستخدام الأزرار
//    const scrollLeftBtn = document.getElementById('scrollPostsLeft');
//    const scrollRightBtn = document.getElementById('scrollPostsRight');

//  scrollLeftBtn.addEventListener('click', () => {
//        postsContainer.scrollBy({
//            left: -scrollAmount,
//            behavior: 'smooth'
//        });
//    clearInterval(autoScrollInterval); // إيقاف التمرير التلقائي عند التمرير اليدوي
//  });

//  scrollRightBtn.addEventListener('click', () => {
//        postsContainer.scrollBy({
//            left: scrollAmount,
//            behavior: 'smooth'
//        });
//    clearInterval(autoScrollInterval); // إيقاف التمرير التلقائي عند التمرير اليدوي
//  });
//});


//    ////////////////////////////////
//    function renderPublishedUsers() {
//  const publishedUsersContainer = document.getElementById('publishedUsersContainer');
//    const publishedUsers = JSON.parse(localStorage.getItem('publishedUsers')) || [];

//    publishedUsersContainer.innerHTML = ""; // إعادة تعيين المحتوى

//  publishedUsers.forEach(user => {
//  const profile = JSON.parse(localStorage.getItem(`profile_${user.email}`));
//    const image = profile?.image || "assets/img/default.png"; // صورة افتراضية إذا لم تكن موجودة

//    const card = `
//    <div class="col-md-3 mb-3 ">
//        <div class="card profile-card border  border-dark text-center rounded-4 user-card bg-light shadow-sm"
//            style="cursor: pointer; transition: transform 0.3s, box-shadow 0.3s; " data-email="${user.email}">

//            <div class="p-3">
//                <img src="${image}" alt="Profile Image" class="rounded-circle mb-3 border border-3 border-dark 
//        shadow-sm" style="width: 250px; height: 250px;   object-fit:fill;">
//                    <h5 class="fw-semibold text-dark">${user.email}</h5>

//                    <div class="d-flex justify-content-center  mt-3 ">
//                        <button class="btn btn-primary px-4 py-2 rounded-pill visit-profile" data-email="${user.email}">
//                            Visit Profile
//                        </button>
//                    </div>
//            </div>

//        </div>
//    </div>
//    `;
//    document.getElementById('publishedUsersContainer').innerHTML += card;
//  });

//  // ✅ إضافة Event Listener لزر "Visit Profile"
//  document.querySelectorAll('.visit-profile').forEach(button => {
//        button.addEventListener('click', function (e) {
//            const email = this.dataset.email;
//            window.location.href = `profilePage.html?email=${email}`;
//        });
//  });
//}

//    // تنفيذ الرندر لعرض المستخدمين المنشورين عند تحميل الصفحة
//    renderPublishedUsers();

/////////profile page/////////////////////


//// Main Script
//    document.addEventListener("DOMContentLoaded", function () {
//  const urlParams = new URLSearchParams(window.location.search);
//    const profileEmail = urlParams.get("email") || localStorage.getItem("currentUser");
//    const currentUser = localStorage.getItem("currentUser");
//    const isOwner = currentUser && profileEmail && currentUser.trim().toLowerCase() === profileEmail.trim().toLowerCase();

//    if (!profileEmail) return;

//    const data = JSON.parse(localStorage.getItem(`profile_${profileEmail}`));
//    if (!data) return;

//    document.getElementById("profile_img").src = data.image || "assets/img/default.png";
//    document.getElementById("name").textContent = `${data.firstName} ${data.lastName}`;
//    document.getElementById("birth").textContent = data.birthDate;
//    document.getElementById("phone").textContent = data.phoneNumber;
//    document.getElementById("gender").textContent = data.gender;
//    document.getElementById("username").textContent=data.user_name;
//    document.getElementById("linkedin").href = data.linkedinUrl || "#";
//    document.getElementById("cv").href = data.cv;


//    // إخفاء إذا مو صاحب البروفايل
//    if (!isOwner) {
//        document.getElementById("editBtn").classList.add("d-none");
//    document.getElementById("addProjectCard").classList.add("d-none");
//    document.getElementById("edit-links-bar").classList.add("d-none");
//  }

//    // عرض المشاريع
//    const projectsContainer = document.getElementById("projectsContainer");
//    const userProjects = JSON.parse(localStorage.getItem(`projects_${profileEmail}`)) || [];

//    if (userProjects.length === 0) {
//        projectsContainer.innerHTML = "<p class='text-muted'>No projects available.</p>";
//  } else {
//        userProjects.forEach(project => {
//            const projectDiv = document.createElement("div");
//            projectDiv.className = "card mb-3";

//            projectDiv.innerHTML = `
//    <div class="card shadow rounded">
//        <div class="card-header bg-success text-white fs-4 fw-bold">
//            <i class="fas fa-folder-open me-2"></i> Project Details
//        </div>
//        <div class="card-body ">
//            <p class="fs-5 ps-2 pt-2 mb-4 textm text-dark">${project.description}</p>
//            <div class="d-flex flex-wrap align-items-center  justify-content-center ps-1 ">
//                <a href="${project.file}" download="${project.fileName}" class="btn btn-success me-2 mb-2 ">
//                    <i class="fas fa-download me-1 "></i> Download File
//                </a>
//                ${isOwner ? `
//                <button class="btn btn-warning me-2 mb-2" onclick="editProject(${project.id})">
//                    <i class="fas fa-edit me-1"></i> Edit
//                </button>
//                <button class="btn btn-danger mb-2" onclick="deleteProject(${project.id})">
//                    <i class="fas fa-trash-alt me-1"></i> Delete
//                </button>
//                ` : ""}
//            </div>
//        </div>
//    </div>
//`;


//            projectsContainer.appendChild(projectDiv);
//        });
//  }
//});

//    // Update Profile Button
//    function editProfile() {
//        window.location.href = "profile.html?edit=true";
//}

//    // Delete Project
//    function deleteProject(projectId) {
//  const currentEmail = localStorage.getItem("currentUser");
//    let projects = JSON.parse(localStorage.getItem(`projects_${currentEmail}`)) || [];
//  projects = projects.filter(p => p.id !== projectId);
//    localStorage.setItem(`projects_${currentEmail}`, JSON.stringify(projects));
//    alert("Project deleted!");
//    location.reload();
//}

//    // Edit Project
//    function editProject(projectId) {
//  const currentEmail = localStorage.getItem("currentUser");
//    const projects = JSON.parse(localStorage.getItem(`projects_${currentEmail}`)) || [];
//  const project = projects.find(p => p.id === projectId);
//    if (!project) return;

//    document.getElementById("des_project").value = project.description;
//    document.getElementById("project_form").classList.remove("d-none");

//    const proSend = document.getElementById("pro_send");
//    proSend.textContent = "Update";

//    proSend.onclick = function (e) {
//        e.preventDefault();
//    const newDescription = document.getElementById("des_project").value;
//    const fileInput = document.getElementById("new_project").files[0];

//    if (fileInput) {
//      const reader = new FileReader();
//    reader.onload = function (e) {
//        project.description = newDescription;
//    project.file = e.target.result;
//    project.fileName = fileInput.name;
//    localStorage.setItem(`projects_${currentEmail}`, JSON.stringify(projects));
//    alert("Project updated!");
//    location.reload();
//      };
//    reader.readAsDataURL(fileInput);
//    } else {
//        project.description = newDescription;
//    localStorage.setItem(`projects_${currentEmail}`, JSON.stringify(projects));
//    alert("Project updated!");
//    location.reload();
//    }
//  };
//}

//    document.addEventListener("DOMContentLoaded", function () {
//  const form = document.getElementById("project_form");
//    if (!form) return;

//    form.addEventListener("submit", async function (e) {
//        e.preventDefault();

//    const description = document.getElementById("des_project").value.trim();
//    const fileInput = document.getElementById("new_project");
//    const file = fileInput.files[0];
//    const currentEmail = localStorage.getItem("currentUser");

//    if (!description || !file) {
//        alert("Please fill in description and upload a file.");
//    return;
//    }

//    const reader = new FileReader();
//    reader.onload = function (event) {
//      const fileData = event.target.result;

//    const existingProjects = JSON.parse(localStorage.getItem(`projects_${currentEmail}`)) || [];

//    const newProject = {
//        id: Date.now(),
//    description: description,
//    file: fileData,
//    fileName: file.name
//      };

//    existingProjects.push(newProject);
//    localStorage.setItem(`projects_${currentEmail}`, JSON.stringify(existingProjects));

//      alert("Project added successfully!");
//      window.location.reload();
//    };

//    reader.readAsDataURL(file);
//  });
//});

