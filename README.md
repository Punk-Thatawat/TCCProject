# TCCProject

โปรเจกต์ระบบ Login/Register ด้วย ASP.NET Core MVC (.NET 10) ใช้ Entity Framework Core, FluentValidation และ JWT สำหรับยืนยันตัวตน

## ฟังก์ชันหลัก

- สมัครสมาชิกด้วย `Username` และ `Password`
- เข้าสู่ระบบด้วย `Username` และ `Password`
- ตรวจสอบข้อมูลด้วย FluentValidation
- สร้างและ validate `JWT Token`
- เก็บ JWT ไว้ใน `HttpOnly Cookie`
- หลัง Login สำเร็จ ระบบจะพาไปหน้า `Home/Index` และแสดงข้อความ `Welcome User : <username>`

## เทคโนโลยีที่ใช้

- ASP.NET Core MVC (.NET 10)
- Entity Framework Core 10
- SQL Server
- FluentValidation
- JWT Bearer Authentication

## โครงสร้างฐานข้อมูล

ตาราง `Users`

- `Id`
- `Username`
- `PasswordHash`
- `CreatedAt`
- `UpdatedAt`

หมายเหตุ:

- รหัสผ่านไม่ได้เก็บเป็น plain text
- ระบบเก็บเป็น `PasswordHash` เพื่อความปลอดภัย

## การตั้งค่า

ไฟล์ [appsettings.json](./appsettings.json) มีค่าหลักที่ต้องใช้ดังนี้

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=TTCTestDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
},
"JwtSettings": {
  "Issuer": "TTCTest",
  "Audience": "TTCTestUsers",
  "SecretKey": "TTCTestJwtSecretKeyForNet10MvcProject2026",
  "ExpiryMinutes": 60
}
```

ถ้าเครื่องของคุณใช้ SQL Server คนละ instance ให้แก้ `DefaultConnection` ให้ตรงก่อนใช้งาน

## วิธีรันโปรเจกต์

1. ติดตั้ง SQL Server หรือ LocalDB ให้พร้อม
2. แก้ `ConnectionStrings:DefaultConnection` ให้ตรงกับเครื่อง
3. รันคำสั่งอัปเดตฐานข้อมูล

```powershell
dotnet ef database update
```

4. รันโปรเจกต์

```powershell
dotnet run
```

5. เปิดเว็บ แล้วเข้าใช้งานหน้า `Login`

## ขั้นตอนการทำงานของระบบ

### Register

1. กรอก `Username`, `Password`, `ConfirmPassword`
2. ระบบตรวจสอบความถูกต้องของข้อมูล
3. ระบบเช็กว่า `Username` ซ้ำหรือไม่
4. ระบบ hash password แล้วบันทึกลงฐานข้อมูล

### Login

1. กรอก `Username` และ `Password`
2. ระบบตรวจสอบความถูกต้องของข้อมูล
3. ระบบตรวจสอบ user/password จากฐานข้อมูล
4. ถ้าถูกต้อง ระบบจะสร้าง JWT
5. JWT จะถูกเก็บใน `HttpOnly Cookie`
6. ระบบพาไปหน้า `Home/Index`
7. หน้า `Home/Index` จะแสดงชื่อผู้ใช้งานจาก token

## โครงสร้างโปรเจกต์หลัก

- `Controllers/` จัดการ MVC actions
- `Models/Db/` entity และ DbContext
- `Models/DTOs/Auth/` ViewModel และ Validator สำหรับ auth
- `Repositories/` data access layer
- `Services/` business logic และ JWT service
- `Migrations/` ประวัติ schema ของฐานข้อมูล
- `Views/` หน้า UI ของ MVC
