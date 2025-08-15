
<template>
    <view class="page">
        <view class="container">
            <view class="user-info">
                <view class="avatar">
                    <image class="avatar-image"
                        src="@/static/logo.svg" />
                </view>
                <text class="nickname">用户</text>
                <text class="role">{{ userStore.userInfo.name }}</text>
            </view>

            <view class="menu-list">
                <view class="menu-item" @click="handleModifyInfo">
                    <text class="menu-text">修改信息</text>
                    <uni-icons type="right" size="16" color="#999" />
                </view>
                <view class="menu-item" @click="handleLogout">
                    <text class="menu-text">退出登陆</text>
                    <uni-icons type="right" size="16" color="#999" />
                </view>
                <view class="menu-item" @click="handleModifyPassword">
                    <view class="menu-left">
                        <uni-icons type="locked" size="20" color="#666" class="menu-icon" />
                        <text class="menu-text">修改密码</text>
                    </view>
                    <uni-icons type="right" size="16" color="#999" />
                </view>
                <view class="menu-item" @click="handleCheckUpdate">
                    <view class="menu-left">
                        <uni-icons type="refresh" size="20" color="#666" class="menu-icon" />
                        <text class="menu-text">版本更新</text>
                    </view>
                    <view class="version-wrap">
                        <text class="version-text">最新版本</text>
                        <uni-icons type="right" size="16" color="#999" />
                    </view>
                </view>
            </view>
        </view>
    </view>
</template>
  
<script lang="ts" setup>
import { ref } from 'vue';
import { useUserStore } from '@/store/user';
const userStore = useUserStore();

const handleModifyInfo = () => {
    uni.showToast({
        title: '修改信息',
        icon: 'none'
    });
};

const handleLogout = () => {
    uni.showModal({
        title: '提示',
        content: '确定要退出登录吗？',
        success: (res) => {
            if (res.confirm) {
                userStore.logout();
                uni.showToast({
                    title: '已退出登录',
                    icon: 'success'
                });
            }
        }
    });
};

const handleModifyPassword = () => {
    uni.showToast({
        title: '修改密码',
        icon: 'none'
    });
};

const handleCheckUpdate = () => {
    uni.showToast({
        title: '检查更新',
        icon: 'none'
    });
};

</script>
  
<style scoped>
page {
    height: 100%;
    background-color: #f5f6fa;
}

.container {    
    height: 100%;
    display: flex;
    flex-direction: column;
}

.header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 20rpx 30rpx;
}

.title {
    font-size: 18px;
    font-weight: 600;
    color: #333;
}

.more-icon {
    width: 24px;
    height: 24px;
}

.user-info {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 40rpx 0;
}

.avatar {
    width: 160rpx;
    height: 160rpx;
    border-radius: 80rpx;
    overflow: hidden;
    margin-bottom: 20rpx;
}

.avatar-image {
    width: 100%;
    height: 100%;
}

.nickname {
    font-size: 16px;
    color: #333;
    margin-bottom: 10rpx;
}

.role {
    font-size: 14px;
    color: #666;
}

.menu-list {
    background-color: #fff;
    border-radius: 20rpx;
    margin: 30rpx;
    padding: 0 30rpx;
}

.menu-item {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 30rpx 0;
    border-bottom: 1px solid #eee;
}

.menu-item:last-child {
    border-bottom: none;
}

.menu-left {
    display: flex;
    align-items: center;
}

.menu-icon {
    width: 20px;
    height: 20px;
    margin-right: 20rpx;
}

.menu-text {
    font-size: 14px;
    color: #333;
}

.version-wrap {
    display: flex;
    align-items: center;
}

.version-text {
    font-size: 12px;
    color: #1976d2;
    margin-right: 10rpx;
}

.tab-bar {
    display: flex;
    justify-content: space-around;
    align-items: center;
    height: 100rpx;
    background-color: #fff;
    border-top: 1px solid #eee;
    margin-top: auto;
}

.tab-item {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 10rpx 0;
}

.tab-text {
    font-size: 12px;
    color: #999;
    margin-top: 6rpx;
}

.active-text {
    color: #1976d2;
}
</style>
  
  