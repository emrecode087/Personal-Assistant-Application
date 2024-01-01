# Personal-Assistant-Application

  Personel Asistan uygulaması bir öğretmenin ders içinde ve ders dışında öğrencilerin devamsızlık durumu , not bilgisi , ders içi katılımı üzerinden işlemler yapmamızı sağlayan bir uygulamadır . Uygulamamdaki amaç öğretmenin işlerini kolaylaştırmak ve zamandan tasarruf etmesini sağlamaktır . 

# Uygulama içi işlemler 
## Login 
  Uygulamaya sadece veritabanındaki kayıtlı öğretmenler , kullanıcı adlarını ve şifreleri ile erişim yapabilirler.

## Menu 
  Menumuzda 3 adet panel bulunmaktadır . İlk panel giriş yapmış olan kullanıcı bilgilerini göstermektedir . Bunlar Name , Surname , e_mail olarak 3 adettir . İkinci panel ise date ve time olarak iki adet bilgiyi içermektedir .  3. panelimiz ise operasyon paneli burada uygulama içindeki ana işlemleri yapmaktayız. Bunlar Classroom , Grades , Reminder , Calculator ve Logout olarak 5 adet mevcuttur.
  
## Classroom  : 
  Bu işlemde ilk önce sınıf seçmekle başlıyoruz . List butonuna tıkladıktan sonra seçili sınıfın veritabanında kayıtlı öğrencileri listview'imize gelir . Daha sonra bu öğrenciler üzerinde işlemler yapabiliriz . Bu işlemler arasında bir tanesi derste olmayan öğrencinin yok yazılmasıdır . Not here butonuyla öğrencinin veritabanındaki devamsızlık durumunu bir arttırıyoruz .  Başka bir işlem de derse katılan öğrenciyi mükafatlandırmak amacıyla extra point komutudur . Bu butona tıkladığımız zaman öğrencinin veritabanında plusPoint verisi bir artıyor , daha sonra öğretmen manuel olarak bu extra pointi final sınavına ekleyebilir.    
## Grades :
  Bu işlemde yine ilk önce sınıfı seçmekle başlıyoruz . DataGridView imizde seçilmiş olan sınıfın öğrencileri ekrana gelir . Daha sonra column üzerinde veritabanı değişiklikleri yapabiliyoruz . Bunlar 3 adettir . Midterm1 , midterm2 ve final sınavlarıdır . Bu sınavları ve öğrenci bilgilerini textbox üzerinde göstererek kullanıcı rahatlığı sunuyoruz . Aynı zamanda bu sayfada öğrencinin eğer 3 notu da girildiyse yıl sonu notu hesaplanır ve harflendirme yapılır . Eğer Classroomda devamsızlık sayısı 4'ten fazla girilmişse bu öğrencinin durumu FAIL olarak belirlenir ve harf notu hesaplanmaz.

# Reminder :
  Bu işlemde öğretmen ders içi ve ders dışı yapacağı şeylerin notunu tutar ve siler . Bu notlar veritabanında saklanır ve kaybolmaz.

# Calculator :
  Klasik hesap makinesi , öğretmenin ders içinde ihtiyaca göre kullanmak isterse diye eklenmiştir.

# Logout :
  Login ekranına dönüş işlemi.
  
  

