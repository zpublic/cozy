import {Page} from 'ionic-angular';

@Page({
  templateUrl: './build/cozyrss/favorite/favorite.html'
})
export class FavoritePage {
  items = [
    {
      title: '由招聘要求看趋势 - VC++程序员路在何方',
      time: '十分钟前',
      source: 'C++博客',
      content: '',
    },
    {
      title: '有效利用标准库提供的type_traits，让程序在编译时作出分支选择',
      time: '昨天 12：21',
      source: 'c++博客',
      content: '',
    },
  ];
}