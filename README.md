# Proje Hakkında
Güncel .Net Core versionu ve paketleri ile temel olması açısından bir çok yaklaşımı ele alarak hazırlanmış bir Rest APİ.
Uygulama olarak Blog uygulaması üzerine code first yaklaşımı ile postgres db kullanılmıştır. Ön yüz olarak [VueJs](https://github.com/necilcakmak/blogui) ile çalışması devam etmektedir.

# Uygulamayı Başlatmak

## Docker Üzerinde
Docker üzerinde çalıştırmak için makinanızda docker ve docker-compose kurulu olması gerekmektedir
Proje ana dizininde
```
docker-compose build
docker-compose up
```

## Local
Localde çalıştırmak için makinanızda Postgres ve .Net Core 6.0 paketlerinin kurulu olması gerekmektedir.

# Kullanılan Teknolojiler
- .Net Core 6.0
- Postgres Database
- Docker & Docker-Compose
- RabbitMQ
- Fluent Validation
- AutoMapper

Bu proje [Necil ÇAKMAK](https://twitter.com/necilcakmak) tarafından yapılmıştır.
