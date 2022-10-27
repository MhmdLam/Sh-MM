//
//  AccessKitDelegate.h
//  AccessKit
//
//  Created by Stefan Fuerst on 19.12.18.
//  Copyright (c) 2018 Media Atelier. All rights reserved.
//


@protocol AccessKitDelegate <NSObject>
@optional
-(void)accessKitWillOpenPanel:(NSString *)panelIdentifier;
-(void)accessKitDidOpenPanel:(NSString *)panelIdentifier;

@end
