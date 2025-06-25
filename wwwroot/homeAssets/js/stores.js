/////////////////////////////////////////////////////////////////////////////////////////////////////////
/* stores  code */
// ==================== إضافة منتج ====================
if (window.location.pathname.includes("add-product.html")) {
    document.addEventListener("DOMContentLoaded", function () {
      const form = document.getElementById("addProductForm");
      if (!form) return;
  
      form.addEventListener("submit", function (e) {
        e.preventDefault();
  
        const name = document.getElementById("name").value.trim();
        const description = document.getElementById("description").value.trim();
        const price = document.getElementById("price").value.trim();
        const imageFile = document.getElementById("image").files[0];
        const ownerEmail = localStorage.getItem("currentUser");
  
        if (!ownerEmail) {
          alert("لم يتم تسجيل الدخول!");
          return;
        }
  
        if (!imageFile) {
          alert("يرجى اختيار صورة للمنتج.");
          return;
        }
  
        const reader = new FileReader();
  
        reader.onload = function () {
          const imageBase64 = reader.result;
  
          const newProduct = {
            id: Date.now(),
            name,
            description,
            price,
            image: imageBase64,
            ownerEmail
          };
  
          const products = JSON.parse(localStorage.getItem("products")) || [];
          products.push(newProduct);
          localStorage.setItem("products", JSON.stringify(products));
  
          alert("تمت إضافة المنتج بنجاح!");
          window.location.href = "stores.html";
        };
  
        reader.readAsDataURL(imageFile);
      });
    });
  }
  
  // ==================== عرض المنتجات ====================
  function loadProducts() {
    const products = JSON.parse(localStorage.getItem("products")) || [];
    const currentEmail = localStorage.getItem("currentUser");
    const container = document.getElementById("productsContainer");
    if (!container) return;
  
    if (products.length === 0) {
      container.innerHTML = `<p class="text-muted text-center">There are no products currently available.</p>`;
      return;
    }
  
    container.innerHTML = "";
  
    products.forEach(product => {
      const col = document.createElement("div");
      col.className = "col-sm-6 col-md-4 col-lg-3 mb-4";
  
      let actionButtons = "";
  
      if (product.ownerEmail === currentEmail) {
        actionButtons = `
          <button class="btn btn-warning btn-sm mb-2 edit-product-btn" data-id="${product.id}">تعديل</button>
          <button class="btn btn-danger btn-sm delete-product-btn" data-id="${product.id}">حذف</button>
        `;
      } else {
        actionButtons = `<button class="btn btn-primary mt-auto add-to-cart-btn" data-id="${product.id}">أضف إلى السلة</button>`;
      }
  
      col.innerHTML = `
        <div class="card h-100 shadow-sm">
          <img src="${product.image}" class="card-img-top" alt="${product.name}" style="height: 200px; object-fit: cover;">
          <div class="card-body d-flex flex-column">
            <h5 class="card-title">${product.name}</h5>
            <p class="card-text">${product.description}</p>
            <p class="text-muted">${product.price} Jd</p>
            <div class="mt-auto">${actionButtons}</div>
          </div>
        </div>
      `;
  
      container.appendChild(col);
    });
  }
  
  if (window.location.pathname.includes("stores.html")) {
    document.addEventListener("DOMContentLoaded", loadProducts);
  }
  
  // ==================== حذف منتج ====================
  document.addEventListener("click", function (e) {
    if (e.target.classList.contains("delete-product-btn")) {
      const productId = parseInt(e.target.getAttribute("data-id"));
      let products = JSON.parse(localStorage.getItem("products")) || [];
  
      if (confirm("هل أنت متأكد من حذف المنتج؟")) {
        products = products.filter(p => p.id !== productId);
        localStorage.setItem("products", JSON.stringify(products));
        loadProducts();
      }
    }
  });
  
  // ==================== تعديل منتج ====================
  document.addEventListener("click", function (e) {
    if (e.target.classList.contains("edit-product-btn")) {
      const productId = e.target.getAttribute("data-id");
      localStorage.setItem("editProductId", productId);
      window.location.href = "edit-product.html";
    }
  });
  
  // ==================== عرض السلة ====================
  function loadCart() {
    const cart = JSON.parse(localStorage.getItem("cart")) || [];
    const container = document.getElementById("cartContainer");
    if (!container) return;
  
    if (cart.length === 0) {
      container.innerHTML = `<p class="text-center text-muted">السلة فارغة.</p>`;
      return;
    }
  
    container.innerHTML = "";
  
    cart.forEach((product, index) => {
      const col = document.createElement("div");
      col.className = "col-sm-6 col-md-4 col-lg-3 mb-4";
  
      col.innerHTML = `
        <div class="card h-100 shadow-sm">
          <img src="${product.image}" class="card-img-top" alt="${product.name}" style="height: 200px; object-fit: cover;">
          <div class="card-body d-flex flex-column">
            <h5 class="card-title">${product.name}</h5>
            <p class="card-text">${product.description}</p>
            <p class="text-muted">${product.price} د.أ</p>
            <button class="btn btn-danger mt-auto remove-from-cart-btn" data-index="${index}">حذف من السلة</button>
          </div>
        </div>
      `;
  
      container.appendChild(col);
    });
  }
  
  if (window.location.pathname.includes("cart.html")) {
    document.addEventListener("DOMContentLoaded", loadCart);
  }
  
  // ==================== حذف من السلة ====================
  document.addEventListener("click", function (e) {
    if (e.target.classList.contains("remove-from-cart-btn")) {
      const index = parseInt(e.target.getAttribute("data-index"));
      const cart = JSON.parse(localStorage.getItem("cart")) || [];
  
      if (index >= 0 && index < cart.length) {
        cart.splice(index, 1);
        localStorage.setItem("cart", JSON.stringify(cart));
        loadCart();
      }
    }
  });
  // main.js

// أدوات عامة
function getLoggedInUserEmail() {
    const user = JSON.parse(localStorage.getItem("currentUser"));
    return user ? user.email : null;
  }
  
  function saveProducts(products) {
    localStorage.setItem("products", JSON.stringify(products));
  }
  
  function getProducts() {
    return JSON.parse(localStorage.getItem("products")) || [];
  }
  
  // ================== add_product.html ==================
  if (window.location.pathname.includes("add_product.html")) {
    const form = document.getElementById("addProductForm");
    const nameInput = document.getElementById("name");
    const descriptionInput = document.getElementById("description");
    const priceInput = document.getElementById("price");
    const imageInput = document.getElementById("image");
  
    // تحقق إذا كنا بنعدل منتج
    const editIndex = localStorage.getItem("editProductIndex");
    if (editIndex !== null) {
      const products = getProducts();
      const product = products[editIndex];
  
      if (product) {
        nameInput.value = product.name;
        descriptionInput.value = product.description;
        priceInput.value = product.price;
        localStorage.setItem("oldImage", product.image); // نخزن الصورة القديمة
      }
    }
  
    form.addEventListener("submit", (e) => {
      e.preventDefault();
  
      const reader = new FileReader();
      const file = imageInput.files[0];
  
      reader.onload = function () {
        const imageURL = file ? reader.result : localStorage.getItem("oldImage");
        const product = {
          name: nameInput.value,
          description: descriptionInput.value,
          price: priceInput.value,
          image: imageURL,
          owner: getLoggedInUserEmail()
        };
  
        const products = getProducts();
  
        if (editIndex !== null) {
          products[editIndex] = product;
          localStorage.removeItem("editProductIndex");
          localStorage.removeItem("oldImage");
        } else {
          products.push(product);
        }
  
        saveProducts(products);
        window.location.href = "store.html";
      };
  
      if (file) {
        reader.readAsDataURL(file);
      } else {
        reader.onload(); // تشغيل الدالة مباشرة إذا ما في صورة جديدة
      }
    });
  }
  
  // ================== store.html ==================
  
function getLoggedInUserEmail() {
  const user = localStorage.getItem("currentUser");
  try {
    const parsed = JSON.parse(user);
    return parsed.email;
  } catch (e) {
    // إذا القيمة هي الإيميل مباشرة
    return user;
  }
}


function saveProducts(products) {
  localStorage.setItem("products", JSON.stringify(products));
}

function getProducts() {
  return JSON.parse(localStorage.getItem("products")) || [];
}
  if (window.location.pathname.includes("store.html")) {
    const container = document.getElementById("productsContainer");
    const currentUserEmail = getLoggedInUserEmail();
    const products = getProducts();
  
    function renderStore() {
      container.innerHTML = "";
      products.forEach((product, index) => {
        const isOwner = product.owner === currentUserEmail;
  
        const card = document.createElement("div");
        card.className = "col-md-4 mb-4";
        card.innerHTML = `
          <div class="card">
            <img src="${product.image}" class="card-img-top" alt="..." style="height: 200px; object-fit: cover;">
            <div class="card-body">
              <h5 class="card-title">${product.name}</h5>
              <p class="card-text">${product.description}</p>
              <p class="card-text"><strong>${product.price} JD</strong></p>
              <div>
                ${
                  isOwner
                    ? `<button class="btn btn-warning btn-sm me-2 edit-btn" data-index="${index}">تعديل</button>
                       <button class="btn btn-danger btn-sm delete-btn" data-index="${index}">حذف</button>`
                    : `<button class="btn btn-primary btn-sm add-to-cart-btn" data-index="${index}">أضف إلى السلة</button>`
                }
              </div>
            </div>
          </div>
        `;
        container.appendChild(card);
      });
  
      // إضافة الأحداث للأزرار
      document.querySelectorAll(".edit-btn").forEach((btn) => {
        btn.addEventListener("click", (e) => {
          const index = e.target.dataset.index;
          localStorage.setItem("editProductIndex", index);
          window.location.href = "add_product.html";
        });
      });
  
      document.querySelectorAll(".delete-btn").forEach((btn) => {
        btn.addEventListener("click", (e) => {
          const index = e.target.dataset.index;
          products.splice(index, 1);
          saveProducts(products);
          renderStore();
        });
      });
  
      document.querySelectorAll(".add-to-cart-btn").forEach((btn) => {
        btn.addEventListener("click", (e) => {
          const index = e.target.dataset.index;
          const cart = JSON.parse(localStorage.getItem("cart")) || [];
          cart.push(products[index]);
          localStorage.setItem("cart", JSON.stringify(cart));
          alert("تمت الإضافة إلى السلة!");
        });
      });
    }
  
    renderStore();
  }
  
  // ================== cart.html ==================
  if (window.location.pathname.includes("cart.html")) {
    const cartContainer = document.getElementById("cartContainer");
    const cart = JSON.parse(localStorage.getItem("cart")) || [];
  
    cartContainer.innerHTML = "";
  
    if (cart.length === 0) {
      cartContainer.innerHTML = "<p class='text-center'>السلة فارغة.</p>";
    } else {
      cart.forEach((product, index) => {
        const card = document.createElement("div");
        card.className = "col-md-4 mb-4";
        card.innerHTML = `
          <div class="card">
            <img src="${product.image}" class="card-img-top" style="height: 200px; object-fit: cover;">
            <div class="card-body">
              <h5 class="card-title">${product.name}</h5>
              <p class="card-text">${product.description}</p>
              <p class="card-text"><strong>${product.price} JD</strong></p>
              <button class="btn btn-danger btn-sm remove-btn" data-index="${index}">إزالة</button>
            </div>
          </div>
        `;
        cartContainer.appendChild(card);
      });
  
      document.querySelectorAll(".remove-btn").forEach((btn) => {
        btn.addEventListener("click", (e) => {
          const index = e.target.dataset.index;
          cart.splice(index, 1);
          localStorage.setItem("cart", JSON.stringify(cart));
          location.reload();
        });
      });
    }
  }
  