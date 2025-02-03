# No 3

1. **Buat branch baru untuk issue production**
    - Setelah ditemukan issue di **Production**, buat branch baru untuk memperbaiki issue tersebut.

2. **Setelah issue production dan fitur A selesai, merge kedua branch ke branch development**
    - Selesaikan fitur A di branch `development` dan perbaikan issue di branch `fix/issue-production`.
    - Setelah selesai, lakukan merge **fix/issue-production** dan **feature A** ke **branch development**.

3. **Di QA, lakukan testing fitur B yang sudah ada di branch development**
    - Di branch **QA**, pastikan **Fitur B** sudah ada di branch `development`.
    - QA akan menguji **fitur A**, **fitur B**, dan **issue produksi** yang telah diperbaiki.

4. **Setelah QA selesai, merge branch QA ke branch production**
    - Setelah pengujian di QA selesai, merge **branch `qa`** ke **branch `production`**.

#
