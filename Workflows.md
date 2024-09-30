# Daily.dev Challenge

## Mô tả công việc
Viết code backend nhằm tạo trang tổng hợp thông tin từ nhiều nguồn khác nhau (tương tự như trang daily.dev) sử dụng SQL Server và C#. 
Dự án hiện tại chỉ tập trung vào backend, chưa quan tâm đến bảo mật hoặc frontend.

### Chức năng chính:
1. Lấy dữ liệu từ các nguồn RSS.
2. Lưu trữ dữ liệu vào cơ sở dữ liệu.
3. Hiển thị dữ liệu lên trang tổng hợp.
4. Cho phép người dùng đăng ký theo dõi các nguồn tin yêu thích.
5. Hiển thị tin tức theo chủ đề người dùng chọn.

## Các Module Chính
### 1. Module lấy dữ liệu:
- Lấy danh sách các nguồn RSS.
- Đọc nội dung và xử lý (parse) dữ liệu.
  
### 2. Module lưu trữ dữ liệu:
- Thiết kế cấu trúc cơ sở dữ liệu.
- Lưu trữ dữ liệu vào cơ sở dữ liệu.
  
### 3. Module logic nghiệp vụ:
- Xử lý yêu cầu người dùng (đăng ký, tìm kiếm).
- Lọc dữ liệu theo yêu cầu người dùng.
  
### 4. Module giao tiếp với frontend (tương lai):
- Cung cấp API để frontend gọi.

## Thiết Kế Cơ Sở Dữ Liệu
- **Dim_Source**: Chứa thông tin về các nguồn tin tức (RSS Feed).
- **Dim_User**: Chứa thông tin về người dùng.
- **Fact_News**: Chứa thông tin các bài viết.
- **Fact_Article_Interaction**: Quản lý tương tác của người dùng với bài viết.
- **Dim_Tag**: Chứa các thẻ (tags) liên quan đến bài viết hoặc người dùng quan tâm.
- **Dim_Category**: Phân loại tin tức theo danh mục lớn (Công nghệ, Đời sống, v.v.).
- **Fact_Bookmark**: Chứa thông tin về các bài viết người dùng đã bookmark.
- **Fact_History**: Chứa thông tin lịch sử đọc bài viết của người dùng.
- **Dim_Date**: Phân tích dữ liệu theo thời gian.
- **News_Tag**: Kết nối bài viết và các thẻ.
- **User_Source**: Kết nối người dùng và các nguồn tin.
- **User_Tag**: Kết nối người dùng và các thẻ.

## Quy Trình Hoạt Động
1. **Lấy Dữ Liệu**: Đọc danh sách các nguồn RSS và lưu vào database.
2. **Lưu Trữ**: Kiểm tra và lưu trữ bài viết mới vào database.
3. **Hiển Thị Dữ Liệu**: Lấy dữ liệu từ database theo yêu cầu người dùng.

## Endpoint API Chính
### NewsController
- `GET /api/news`: Lấy danh sách tất cả bài viết tin tức.
- `GET /api/news/{id}`: Lấy chi tiết bài viết theo ID.
- `POST /api/news`: Tạo mới bài viết.
- `PUT /api/news/{id}`: Cập nhật bài viết.
- `DELETE /api/news/{id}`: Xóa bài viết.

### SourceController
- `GET /api/source`: Lấy danh sách tất cả các nguồn tin.
- `GET /api/source/{id}`: Lấy chi tiết nguồn tin.
- `POST /api/source`: Tạo nguồn tin mới.
- `PUT /api/source/{id}`: Cập nhật nguồn tin.
- `DELETE /api/source/{id}`: Xóa nguồn tin.

### UserController
- `GET /api/user`: Lấy danh sách người dùng.
- `GET /api/user/{id}`: Lấy chi tiết người dùng.
- `POST /api/user`: Tạo người dùng mới.
- `PUT /api/user/{id}`: Cập nhật người dùng.
- `DELETE /api/user/{id}`: Xóa người dùng.

### TagController
- `GET /api/tag`: Lấy danh sách thẻ.
- `GET /api/tag/{id}`: Lấy chi tiết thẻ.
- `POST /api/tag`: Tạo thẻ mới.
- `PUT /api/tag/{id}`: Cập nhật thẻ.
- `DELETE /api/tag/{id}`: Xóa thẻ.

### InteractionController
- `GET /api/interaction`: Lấy danh sách tất cả các tương tác.
- `POST /api/interaction`: Tạo mới tương tác.
- `PUT /api/interaction/{id}`: Cập nhật tương tác.
- `DELETE /api/interaction/{id}`: Xóa tương tác.

### BookmarkController
- `POST /api/bookmark`: Thêm bookmark mới.
- `GET /api/bookmark/user/{userId}`: Lấy danh sách bookmark của người dùng.
- `DELETE /api/bookmark/{id}`: Xóa bookmark.

### HistoryController
- `POST /api/history`: Thêm lịch sử đọc.
- `GET /api/history/user/{userId}`: Lấy lịch sử đọc của người dùng.

### CategoryController
- `GET /api/category`: Lấy danh sách danh mục.
- `POST /api/category`: Tạo danh mục mới.
- `PUT /api/category/{id}`: Cập nhật danh mục.
- `DELETE /api/category/{id}`: Xóa danh mục.