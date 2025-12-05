# Proje Hakkında
Güncel .Net versionu ve paketleri ile temel olması açısından bir çok yaklaşımı ele alarak hazırlanmış bir Rest APİ.
Uygulama olarak Blog uygulaması üzerine code first yaklaşımı ile postgres db kullanılmıştır. Ön yüz olarak [React](https://github.com/necilcakmak/blogui) ile çalışması devam etmektedir.

# Uygulamayı Başlatmak

## Docker Üzerinde
Docker üzerinde çalıştırmak için makinanızda docker ve docker-compose kurulu olması gerekmektedir
Proje ana dizininde
```
docker-compose build
docker-compose up
```

## Jenkins
Ek olarak Jenkins ile CI/CD pipeline hazırlanmıştır. İstenir ise Jenkins compose çalıştırılarak ilgili ayarlar yapıldıktan sonra ngrok ile public ip ile github webhook sağlanabilir
Jenkins compose
```
docker-compose -f jenkins-compose.yml up -d
```

ngrok için
```
ngrok config add-authtoken <token>
ngrok http 8080
```

## Local
Localde çalıştırmak için makinanızda Postgres, .Net10, Redis ve RabbitMQ paketlerinin kurulu olması gerekmektedir. Redis için Docker, RabbitMQ için cloud kullanabilirsiniz.

# Kullanılan Teknolojiler
- .Net10
- Postgres Database
- Docker & Docker-Compose
- RabbitMQ
- Redis
- AutoMapper

