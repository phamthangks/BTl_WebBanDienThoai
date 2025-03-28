function loadReviewForm(productId) {
    $.get(`/api/Review/CreateReview/${productId}`, function(response) {
        $('#reviewFormContainer').html(response);
        initializeStarRating();
    }).fail(function(error) {
        console.error('Error loading review form:', error);
        $('#reviewFormContainer').html('<div class="alert alert-danger">Có lỗi xảy ra khi tải form đánh giá</div>');
    });
}

function initializeStarRating() {
    const stars = document.querySelectorAll('.star-rating .fa-star');
    const rateInput = document.querySelector('#selectedRate');

    stars.forEach(star => {
        star.addEventListener('mouseover', function() {
            const rate = this.dataset.rate;
            stars.forEach((s, index) => {
                if (index < rate) {
                    s.classList.remove('far');
                    s.classList.add('fas');
                } else {
                    s.classList.remove('fas');
                    s.classList.add('far');
                }
            });
        });

        star.addEventListener('mouseout', function() {
            const selectedRate = parseInt(rateInput.value) || 0;
            stars.forEach((s, index) => {
                if (index < selectedRate) {
                    s.classList.remove('far');
                    s.classList.add('fas');
                } else {
                    s.classList.remove('fas');
                    s.classList.add('far');
                }
            });
        });

        star.addEventListener('click', function() {
            const rate = this.dataset.rate;
            rateInput.value = rate;
            stars.forEach((s, index) => {
                if (index < rate) {
                    s.classList.remove('far');
                    s.classList.add('fas');
                } else {
                    s.classList.remove('fas');
                    s.classList.add('far');
                }
            });
        });
    });
}

function loadReviews(productId) {
    $.ajax({
        url: `/api/Review/productreview/${productId}`,
        method: 'GET',
        success: function(data) {
            var reviewsContainer = $('#reviewsContainer');
            reviewsContainer.empty();

            // Hiển thị thông báo trạng thái
            var statusHtml = '';
            if (!data.isLoggedIn) {
                statusHtml = `
                    <div class="alert alert-warning">
                        <i class="fas fa-exclamation-circle"></i>
                        Vui lòng <a href="/Access/Login">đăng nhập</a> để đánh giá sản phẩm.
                    </div>`;
            } else if (!data.hasPurchased) {
                statusHtml = `
                    <div class="alert alert-info">
                        <i class="fas fa-info-circle"></i>
                        Bạn cần mua sản phẩm này để có thể đánh giá.
                    </div>`;
            }
            if (statusHtml) {
                $('#reviewFormContainer').html(statusHtml);
            }

            // Hiển thị danh sách đánh giá
            if (data.reviews && data.reviews.length > 0) {
                var reviewHtml = `<h4 class="mb-4">${data.reviews.length} đánh giá cho "${data.dmSp.tenSanPham}"</h4>`;
                data.reviews.forEach(function(review) {
                    reviewHtml += `
                        <div class="media mb-4">
                            <img src="/images/user-avatar.png" alt="Avatar" class="img-fluid mr-3 mt-1" style="width: 45px;">
                            <div class="media-body">
                                <h6>${review.tenDangNhap}<small> - <i>${new Date(review.thoiGianDanhGia).toLocaleDateString()}</i></small></h6>
                                <div class="text-primary mb-2">
                                    ${generateStars(review.rate)}
                                </div>
                                <p>${review.noiDung}</p>
                                ${review.isCurrentUser ? `
                                    <div class="mt-2">
                                        <button class="btn btn-sm btn-primary" onclick="editReview('${review.maSanPham}')">
                                            <i class="fas fa-edit"></i> Sửa
                                        </button>
                                        <button class="btn btn-sm btn-danger" onclick="deleteReview('${review.maSanPham}')">
                                            <i class="fas fa-trash"></i> Xóa
                                        </button>
                                    </div>
                                ` : ''}
                            </div>
                        </div>
                    `;
                });
                reviewsContainer.html(reviewHtml);
            } else {
                reviewsContainer.html(`
                    <div class="review-empty w-100">
                        <div class="alert alert-info">
                            <i class="fas fa-info-circle"></i>
                            Sản phẩm này chưa có đánh giá nào.
                        </div>
                    </div>
                `);
            }
        },
        error: function(error) {
            console.error('Error loading reviews:', error);
            $('#reviewsContainer').html('<div class="alert alert-danger">Có lỗi xảy ra khi tải đánh giá</div>');
        }
    });
}

function generateStars(rate) {
    return [...Array(5)].map((_, i) => 
        `<i class="fa${i < rate ? 's' : 'r'} fa-star"></i>`
    ).join('');
}

function handleReviewSubmit(event) {
    event.preventDefault();
    
    const form = event.target;
    const formData = {
        maSanPham: form.querySelector('[name="maSanPham"]').value,
        rate: parseInt(form.querySelector('#selectedRate').value),
        noiDung: form.querySelector('#reviewContent').value
    };

    $.ajax({
        url: '/api/review/submitreview',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function(response) {
            if (response.success) {
                loadReviews(formData.maSanPham);
                form.reset();
            } else {
                alert(response.message || 'Có lỗi xảy ra khi gửi đánh giá');
            }
        },
        error: function(xhr) {
            alert('Có lỗi xảy ra khi gửi đánh giá');
        }
    });
}

function deleteReview(productId) {
    if (confirm('Bạn có chắc muốn xóa đánh giá này?')) {
        $.ajax({
            url: `/api/review/deletereview/${productId}`,
            type: 'POST',
            success: function(response) {
                if (response.success) {
                    loadReviews(productId);
                    loadReviewForm(productId);
                } else {
                    alert(response.message);
                }
            }
        });
    }
}

function editReview(productId) {
    $.ajax({
        url: `/api/Review/GetReviewForEdit/${productId}`,
        method: 'GET',
        success: function(response) {
            if (response.success) {
                const review = response.review;
                // Cập nhật form hiện tại thành form edit
                const formContainer = $('#reviewFormContainer');
                formContainer.html(`
                    <div class="mb-4">
                        <h4>Cập nhật đánh giá</h4>
                        <form id="reviewForm" onsubmit="handleUpdateReview(event)">
                            <input type="hidden" name="maSanPham" value="${productId}" />
                            
                            <div class="d-flex my-3">
                                <p class="mb-0 mr-2">Đánh giá * :</p>
                                <div class="star-rating">
                                    ${[1,2,3,4,5].map(i => `
                                        <i class="fa-star ${i <= review.rate ? 'fas' : 'far'}" 
                                           data-rate="${i}" 
                                           style="cursor: pointer;"></i>
                                    `).join('')}
                                </div>
                                <input type="hidden" id="selectedRate" name="rate" value="${review.rate}" />
                            </div>

                            <div class="form-group">
                                <label for="reviewContent">Nội dung đánh giá *</label>
                                <textarea id="reviewContent" 
                                          name="noiDung"
                                          class="form-control" 
                                          rows="5" 
                                          required>${review.noiDung}</textarea>
                            </div>

                            <div class="form-group mb-0">
                                <button type="submit" class="btn btn-primary px-3">
                                    <i class="fas fa-save"></i> Cập nhật
                                </button>
                                <button type="button" class="btn btn-danger px-3 ml-2" onclick="deleteReview('${productId}')">
                                    <i class="fas fa-trash"></i> Xóa
                                </button>
                            </div>
                        </form>
                    </div>
                `);
                initializeStarRating();
            } else {
                alert(response.message || 'Không thể tải đánh giá để chỉnh sửa');
            }
        },
        error: function(error) {
            console.error('Error loading review for edit:', error);
            alert('Có lỗi xảy ra khi tải đánh giá để chỉnh sửa');
        }
    });
}

function handleUpdateReview(event) {
    event.preventDefault();
    
    const form = event.target;
    const formData = {
        maSanPham: form.querySelector('[name="maSanPham"]').value,
        rate: parseInt(form.querySelector('#selectedRate').value),
        noiDung: form.querySelector('#reviewContent').value
    };

    $.ajax({
        url: '/api/review/updatereview',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function(response) {
            if (response.success) {
                loadReviews(formData.maSanPham);
                loadReviewForm(formData.maSanPham);
            } else {
                alert(response.message || 'Có lỗi xảy ra khi cập nhật đánh giá');
            }
        },
        error: function(xhr) {
            alert('Có lỗi xảy ra khi cập nhật đánh giá');
        }
    });
}

document.addEventListener('DOMContentLoaded', function() {
    initializeStarRating();
}); 