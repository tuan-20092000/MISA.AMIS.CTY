<template>
  <div class="amis-header">
    <div class="three-stripes"></div>
    <div class="tooltip">
      <div class="tooltip-content">CÔNG TY CỔ PHẦN MISA</div>
      <img
        class="img-expand_more"
        src="../Resource/img/expland-more-big.svg"
        alt="img-expand_more"
      />
    </div>

      <div class="div-space"></div>

    <div class="div-search">
      <input type="text" placeholder="Nhập từ khóa tìm kiếm" />
      <img src="../Resource/img/find.svg" alt="search" />
    </div>

    <div class="header-menu-icon">
      <img src="../Resource/img/group.svg" alt="more_svg" />
    </div>

    <div class="header-menu-icon">
      <img src="../Resource/img/setting.svg" alt="more_svg" />
    </div>

    <div class="header-menu-icon">
      <img src="../Resource/img/support.svg" alt="more_svg" />
    </div>

    <div class="header-menu-icon">
      <img src="../Resource/img/notify.svg" alt="more_svg" />
    </div>

    <div class="div-username">
      <div class="header-menu-img">
        <img src="../Resource/avatar-default.png" alt="more_svg" />
      </div>
      <div>
        Nguyễn Văn Tuấn
      </div>
      <div>
        <img
          class="img-expand_more"
          src="../Resource/img/expand_more.svg"
          alt="img-expand_more"
        />
      </div>
    </div>
    
  </div>
</template>

<script>
export default {
  name: "Header",
  data() {
    return {};
  },
};
</script>

<style src="../css/googlefont.css"></style>
<style scope src="../css/header.css"></style>

