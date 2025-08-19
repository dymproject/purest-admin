
<template>
  <view class="page">
    <view class="container">
      <scroll-view class="content" scroll-y enable-flex>
        <view class="product-manage">
          <text class="section-title">产品管理</text>
          <view class="stat-cards">
            <view class="card">
              <image class="card-img" src="@/static/index/in-warehouse.jpg" mode="aspectFill" />
              <text class="card-text">入库</text>
            </view>
            <view class="card">
              <image class="card-img" src="@/static/index/out-warehouse.jpg" mode="aspectFill" />
              <text class="card-text">出库</text>
            </view>
            <view class="card">
              <image class="card-img" src="@/static/index/product-warehouse.jpg" mode="aspectFill" />
              <text class="card-text">产品库</text>
            </view>
          </view>
        </view>

        <view class="warehouse-manage">
          <text class="section-title">仓库管理</text>
          <view class="stat-cards">
            <view class="card">
              <uni-icons type="undo" size="32" color="#ffc107" />
              <text class="card-text">仓库</text>
            </view>
            <view class="card">
              <uni-icons type="checkbox" size="32" color="#4caf50" />
              <text class="card-text">开始盘点</text>
            </view>
            <view class="card">
              <uni-icons type="redo" size="32" color="#ff5722" />
              <text class="card-text">库存调拨</text>
            </view>
          </view>
        </view>

        <view class="statistics">
          <text class="section-title">统计报表</text>
          <view class="stat-cards">
            <view class="card">
              <uni-icons type="upload" size="32" color="#4caf50" />
              <text class="card-text">出库报表</text>
            </view>
            <view class="card">
              <uni-icons type="download" size="32" color="#2196f3" />
              <text class="card-text">入库报表</text>
            </view>
            <view class="card">
              <uni-icons type="shop" size="32" color="#ff9800" />
              <text class="card-text">商品出库</text>
            </view>
            <view class="card">
              <uni-icons type="cart" size="32" color="#ffc107" />
              <text class="card-text">商品入库</text>
            </view>
            <view class="card">
              <uni-icons type="home" size="32" color="#ff5722" />
              <text class="card-text">仓库统计</text>
            </view>
            <view class="card">
              <uni-icons type="wallet" size="32" color="#4caf50" />
              <text class="card-text">存货统计</text>
            </view>
          </view>
        </view>
      </scroll-view>

      <!-- <view class="tab-bar">
      <view class="tab-item active">
        <uni-icons type="home" size="24" color="#ffffff"/>
        <text class="tab-text">首页</text>
      </view>
      <view class="tab-item">
        <uni-icons type="list" size="24" color="#ffffff"/>
        <text class="tab-text">单据</text>
      </view>
      <view class="tab-item">
        <uni-icons type="person" size="24" color="#ffffff"/>
        <text class="tab-text">我的</text>
      </view>
    </view> -->
    </view>
  </view>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
</script>

<style scoped>
page {
  height: 100%;
  background-color: #f5f6fa;
}

.container {
  display: flex;
  flex-direction: column;
  background-color: #f5f5f5;
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 20rpx 30rpx;
  background-color: #ffffff;
}

.title {
  font-size: 18px;
  font-weight: bold;
  color: #333333;
}

.todo {
  display: flex;
  align-items: center;
}

.todo-text {
  font-size: 14px;
  color: #666666;
  margin-right: 10rpx;
}

.todo-num {
  background-color: #ff5722;
  color: #ffffff;
  font-size: 12px;
  padding: 4rpx 12rpx;
  border-radius: 20rpx;
}

.content {
  flex: 1;
  overflow: auto;
  padding: 20rpx;
  box-sizing: border-box;
}

.section-title {
  font-size: 16px;
  font-weight: bold;
  color: #333333;
  margin-bottom: 20rpx;
}

.section-desc {
  font-size: 14px;
  color: #666666;
  margin-bottom: 20rpx;
}

.product-manage,
.warehouse-manage,
.statistics {
  background-color: #ffffff;
  border-radius: 20rpx;
  padding: 30rpx;
  margin-bottom: 20rpx;
}


.stat-cards {
  display: flex;
  flex-wrap: wrap;
  gap: 30rpx;
  margin-top: 20rpx;
}

.card {
  width: calc((100% - 60rpx) / 3);
  /* 3列，2个gap */
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 20rpx;
  box-sizing: border-box;
  background-color: #f9f9f9;
  border-radius: 16rpx;
}

.card-img {
  width: 120rpx;
  height: 120rpx;
  margin-bottom: 20rpx;
}

.card-text {
  font-size: 14px;
  color: #666666;
}

.tab-bar {
  display: flex;
  justify-content: space-around;
  padding: 20rpx 0;
  background: linear-gradient(to right, #1976d2, #2196f3);
}

.tab-item {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.tab-text {
  font-size: 12px;
  color: #ffffff;
  margin-top: 10rpx;
}

.active {
  opacity: 1;
}
</style>

